所有存储过程参数定义
	@strFields			varchar(MAX)='',
	@strFieldValues		varchar(MAX)='',
	@strEditSql			varchar(MAX)='',
	@Key_Ids			varchar(MAX)='',
	@strSplits			varchar(MAX)='',	
	@ImpNumber			varchar(100)='',

	@CDateSt			varchar(20)='',
	@CDateEd			varchar(20)='',
	@UImg				bit=0,	
	@State				bit=0,

	@Key_Id				decimal(18,0)=-1, 
	@Ord_Id				decimal(18,0)=-1,
	@CluNoState			bit=1,
	@CUserId			int=-1,
	@EUser_Id			int=-1,
	@EDept_Id			int=-1,
	@Fy_Id				int=-1,
	@flag				int=1 


查询页面：
1、查询
	SELECT A.* INTO #TEM
	FROM Sys_BuyOrders A  
	WHERE
	(@CDateSt='' OR A.BOrdDate>=@CDateSt)
	AND (@CDateEd='' OR A.BOrdDate<=@CDateEd) 
	AND (@CluNoState=1 OR @CluNoState=0 AND A.BOrdState<>0)
	AND (@Key_Id=-1 OR A.Buy_Id=@Key_Id)
    AND (@BOrdState='-1' OR A.BOrdState=@BOrdState)
	AND (@BOrdNumber='' OR A.BOrdNumber like '%'+@BOrdNumber+'%')
	
	--主表数据源
	SELECT A.*
	FROM #TEM A
	ORDER BY A.Buy_Id DESC 

	--嵌套子表数据源
	SELECT B.*
	FROM #TEM A 
	INNER JOIN Sys_BuyOrdersInfo B ON A.Buy_Id=B.Buy_Id
	WHERE B.State=1

2、显示详细
	--gridVInfo数据源：嵌套主表
	SELECT A.*
	FROM [Tst_ProductJkOrd] O 
	INNER JOIN [Tst_Product] A ON A.[ProdJk_Id]=O.[ProdJk_Id]
	WHERE O.[ProdJk_Id]=@Key_Id AND A.State=1
	
	--gridVInfo数据源：嵌套子表
	SELECT A.* 	  
	      ,B.StylePic
		  ,B.Pic_Version
		  ,B.PNumber
		  ,B.Name AS PName,pc.Name as pcName,spc.Name as spcName
	FROM [Tst_ProductJkOrd] O 
	INNER JOIN [Tst_Product] T ON T.[ProdJk_Id]=O.[ProdJk_Id]
	INNER JOIN [Tst_ProductInfo] A ON A.[Prod_Id]=T.[Prod_Id] 
	LEFT OUTER JOIN [V_SYS_PM] B ON B.Pm_Id=A.Pm_Id
	LEFT OUTER JOIN [V_Product_Class100] pc ON B.Pc_Id=pc.Pc_Id
	LEFT OUTER JOIN V_Sub_Product_Class100 spc ON B.Spc_Id=spc.Spc_Id
	WHERE O.[ProdJk_Id]=@Key_Id AND T.State=1 AND A.State=1	 
	ORDER BY A.BarCode
	
	--gridBInfo单一无嵌套数据源
	SELECT A.* 	  
	      ,B.StylePic
		  ,B.Pic_Version
		  ,B.PNumber
		  ,B.Name AS PName,pc.Name as pcName,spc.Name as spcName
	FROM [Tst_ProductJkOrd] O 
	INNER JOIN [Tst_Product] T ON T.[ProdJk_Id]=O.[ProdJk_Id]
	INNER JOIN [Tst_ProductInfo] A ON A.[Prod_Id]=T.[Prod_Id] 
	LEFT OUTER JOIN [V_SYS_PM] B ON B.Pm_Id=A.Pm_Id
	LEFT OUTER JOIN [V_Product_Class100] pc ON B.Pc_Id=pc.Pc_Id
	LEFT OUTER JOIN V_Sub_Product_Class100 spc ON B.Spc_Id=spc.Spc_Id
	WHERE O.[ProdJk_Id]=@Key_Id AND T.State=1 AND A.State=1	 
	ORDER BY A.BarCode
	 
	SELECT DSNME='gridVInfo-Main,gridVInfo-Com,gridBInfo'

3、打印BarTender标签
	--除主键MarkOrdInfo_Id外，字段都传入BarTender模板
	SELECT A.MarkOrdInfo_Id,A.Barcode,A.NetWeight,A.AllWeight
      ,B.Name,PC.SetText AS PqName --StylePic,Pic_Version,--如果要打印图片需返回
	FROM Sys_MarkOrdInfo AS A
	LEFT OUTER JOIN [V_SYS_PM] B ON B.Pm_Id=A.Pm_Id
	INNER JOIN [dbo].[StringSplitToTable](@Key_Ids,',')AS Ids ON Ids.Id=A.MarkOrdInfo_Id
	LEFT OUTER JOIN Bse_Set PC ON PC.SetValue=A.Pq_Id AND PC.SetKey='PmQuality'
	WHERE A.State=1

4、更新状态
	IF @strEditSql<>''
	BEGIN
		SET @EXEC_SQL = 'UPDATE Sys_BuyOrders SET ' + @strEditSql
		SET @EXEC_SQL = @EXEC_SQL + ' WHERE Buy_Id IN (' + @Key_Ids + ')'
		EXEC (@EXEC_SQL)
	END
	UPDATE Sys_BuyOrders SET
		EDt=GETDATE(),EUserId=@EUser_Id	
		,BOrdState=0,DelDt=GETDATE(),DelUserId=@EUser_Id
	FROM Sys_BuyOrders A
	INNER JOIN StringSplitToTable(@Key_Ids,',')AS B ON B.Id=A.Buy_Id

	SELECT TOP 1 Remark,BOrdState=0,UpdateFields='Remark,BOrdState'		
	FROM Sys_BuyOrders A
	INNER JOIN StringSplitToTable(@Key_Ids,',')AS B ON B.Id=A.Buy_Id

5、批量修改
	SET @EXEC_SQL = 'UPDATE Sys_OrdersInfoNew SET ' + @strEditSql
	SET @EXEC_SQL = @EXEC_SQL + ' WHERE OrdInfo_Id IN (' + CAST(@Key_Ids AS VARCHAR)+')'
	EXEC (@EXEC_SQL)

	--返回修改后数据源，需要与获取对应明细数据源相同架构
	SELECT  
	 A.* 
	  ,B.StylePic
	  ,B.Pic_Version
	  ,B.PNumber
	  ,B.Name
	FROM Sys_OrdersInfoNew  A
	INNER JOIN V_SYS_PM B ON B.Pm_Id=A.Pm_Id
 	INNER JOIN StringSplitToTable(@Key_Ids,',') AS C ON A.OrdInfo_Id=C.Id


业务编辑页面：
1、新增主表
	INSERT INTO #table EXEC Get_Sys_Series_Number 'MK','Sys_MarkOrd','MarkNumber',2,@Fy_Id  
	SELECT @Number=Number FROM #table 
	DROP TABLE #table
	SET @Number=SUBSTRING(@Number,1,2)+CAST(@Fy_Id AS VARCHAR)+SUBSTRING(@Number,3,6)+SUBSTRING(@Number,9,3)

	set @sql = 'INSERT INTO Sys_MarkOrd(' + @strFields + ',MarkNumber,Fy_Id,Dept_Id,CUserId) VALUES(' + @strFieldValues+','''+@Number+''','+CAST(@Fy_Id AS VARCHAR)+','+CAST(@EDept_Id AS VARCHAR)+','+CAST(@EUser_Id AS VARCHAR) + ')' + ';SELECT @idA=@@identity' 
	exec sp_executesql @sql,N'@idA decimal output',@id output

	UPDATE Sys_MarkOrd SET 
		MarkDate=CONVERT([varchar](10),a.MarkDt,(120)) 
	FROM Sys_MarkOrd A
	WHERE MarkOrd_Id=@id

	SELECT MarkOrd_Id,MarkNumber,MarkDt,UpdateFields='MarkNumber,MarkDt'
	FROM Sys_MarkOrd A
	WHERE MarkOrd_Id=@id

2、修改主表
	SET @EXEC_SQL = 'UPDATE Sys_MarkOrd SET ' + @strEditSql
	SET @EXEC_SQL = @EXEC_SQL + ' WHERE MarkOrd_Id=' + CAST(@Key_Id AS VARCHAR)
	EXEC (@EXEC_SQL)

	UPDATE Sys_MarkOrd SET 
		MarkDate=CONVERT([varchar](10),a.MarkDt,(120)) 
	FROM Sys_MarkOrd A
	WHERE MarkOrd_Id=@Key_Id

3、复制主表
	SELECT @Key_Id=Ord_Id
	FROM Sys_OrdersNew A
	WHERE A.OrdNumber=@ImpNumber AND Fy_Id=@Fy_Id

	IF @Key_Id IS NULL OR @Key_Id=-1
	BEGIN
		SELECT ERROR='不存在对应单号的订单，请检查.'
		RETURN
	END
	BEGIN TRY 
		BEGIN TRAN
			INSERT INTO #TABLE EXEC Get_Sys_Series_Number 'ORD','Sys_OrdersNew','OrdNumber',3,@Fy_Id 
			SELECT @Number=Number FROM #TABLE 
			DROP TABLE #TABLE
--			SET @Number=SUBSTRING(@Number,1,3)+CAST(@Fy_Id AS VARCHAR)+SUBSTRING(@Number,4,6)+SUBSTRING(@Number,10,3)

			INSERT INTO Sys_OrdersNew(IndetOrdNum,PutDate,PutDt
				,CustId,CustType,CustSignet,FySignet
				,Fy_Id,Dept_Id,OrdType,OrdNumber
				,Total_Weight,Total_Amount,Total_WeightNoOut,Total_AmountNoOut,Remark,CUserId)
			SELECT IndetOrdNum,PutDate,PutDt
				,CustId,CustType,CustSignet,FySignet
				,@Fy_Id,@EDept_Id,OrdType,@Number
				,Total_Weight,Total_Amount,Total_Weight,Total_Amount,Remark,@EUser_Id
			FROM Sys_OrdersNew A
			WHERE A.Ord_Id=@Key_Id 

			SELECT @Ord_Id=@@IDENTITY			
			
			INSERT INTO Sys_OrdersInfoNew(Ord_Id,Pm_Id,PmQuality
				,Amount,Remark,AmountNoOut,MinWeight,MaxWeight,PmGoodsType,TecType,TecRemark,Length,Width,Diameter,Rang)
			SELECT @Ord_Id,A.Pm_Id ,A.PmQuality
				,A.Amount,A.ReMark,A.Amount
				,MinWeight,MaxWeight,PmGoodsType,TecType,TecRemark,Length,Width,Diameter,Rang
			FROM Sys_OrdersInfoNew A
			WHERE A.Ord_Id=@Key_Id AND A.State=1			

			SELECT A.*
			,C.CustName
			,C.CustKey
			FROM Sys_OrdersNew A 
			INNER JOIN VCustomer C ON C.CustId=A.CustId AND C.CustType=A.CustType
			WHERE Ord_Id=@Ord_Id
		COMMIT TRAN
	END TRY 
	BEGIN CATCH 
		IF (@@ERROR<>0)
			BEGIN
				ROLLBACK TRAN
				SELECT ERROR=ERROR_MESSAGE()
			END
	END CATCH

4、更新状态
	IF @strEditSql<>''
	BEGIN
		SET @EXEC_SQL = 'UPDATE Sys_BuyOrders SET ' + @strEditSql
		SET @EXEC_SQL = @EXEC_SQL + ' WHERE Buy_Id='+CAST(@Key_Id AS VARCHAR)
		EXEC (@EXEC_SQL)
	END

	UPDATE Sys_BuyOrders SET
		EDt=GETDATE(),EUserId=@EUser_Id	
		,BOrdState=50,ConfirmDt=GETDATE(),ConfirmUserId=@EUser_Id
		,ReturnDt=NULL,ReturnUserId=NULL	
	FROM Sys_Orders A
	WHERE Buy_Id=@Key_Id

	SELECT BOrdState=50

5、新增子表
	SELECT @OrdNumber=MarkType FROM Sys_MarkOrd WHERE MarkOrd_Id=@Ord_Id
	INSERT INTO #table EXEC Get_Sys_Series_Number @OrdNumber,'Sys_MarkOrdInfo','Barcode',4,@Fy_Id  
	SELECT @Number=Number FROM #table 
	DROP TABLE #table

	set @sql = 'INSERT INTO Sys_MarkOrdInfo(' + @strFields + ',Barcode) VALUES(' + @strFieldValues+','''+@Number+''''+')' + ';SELECT @idA=@@identity' 
	exec sp_executesql @sql,N'@idA decimal output',@id output

	IF EXISTS(SELECT * FROM Sys_MarkOrdInfo WHERE MarkOrdInfo_Id=@id AND AllWeight>0 AND AllWeight<NetWeight)
	BEGIN
		ROLLBACK TRAN
		SELECT ERROR='货重不能小于净重.'
		RETURN
	END

	UPDATE Sys_MarkOrdInfo SET
		AllWeight=NetWeight
	FROM Sys_MarkOrdInfo
	WHERE MarkOrdInfo_Id=@id AND AllWeight=0

	UPDATE Sys_MarkOrd SET 
		NetWeight=B.NetWeight
		,AllWeight=B.AllWeight
		,Amount=B.Amount
	FROM Sys_MarkOrd A
	INNER JOIN
	(
		SELECT MarkOrd_Id
			,NetWeight=SUM(NetWeight)
			,AllWeight=SUM(AllWeight)
			,Amount=SUM(1)
		FROM Sys_MarkOrdInfo 
		WHERE MarkOrd_Id=@Ord_Id AND State=1
		GROUP BY MarkOrd_Id
	)AS B ON B.MarkOrd_Id=A.MarkOrd_Id
	WHERE A.MarkOrd_Id=@Ord_Id	

	SELECT MarkOrdInfo_Id,Barcode,AllWeight,
		UpdateFields='Barcode,AllWeight'
	FROM Sys_MarkOrdInfo
	WHERE MarkOrdInfo_Id=@id

	SELECT A.NetWeight,AllWeight,Amount
		,UpdateFieldsOrd='NetWeight,AllWeight,Amount'
	FROM Sys_MarkOrd A
	WHERE A.MarkOrd_Id=@Ord_Id 

6、修改子表
	SET @EXEC_SQL = 'UPDATE Sys_MarkOrdInfo SET ' + @strEditSql
	SET @EXEC_SQL = @EXEC_SQL + ' WHERE MarkOrdInfo_Id=' + CAST(@Key_Id AS VARCHAR)
	EXEC (@EXEC_SQL)	

	IF EXISTS(SELECT * FROM Sys_MarkOrdInfo WHERE MarkOrdInfo_Id=@Key_Id AND AllWeight>0 AND AllWeight<NetWeight)
	BEGIN
		ROLLBACK TRAN
		SELECT ERROR='货重不能小于净重.'
		RETURN
	END

	UPDATE Sys_MarkOrdInfo SET
		AllWeight=NetWeight
	FROM Sys_MarkOrdInfo
	WHERE MarkOrdInfo_Id=@Key_Id AND AllWeight=0

	UPDATE Sys_MarkOrd SET 
		NetWeight=B.NetWeight
		,AllWeight=B.AllWeight
		,Amount=B.Amount
	FROM Sys_MarkOrd A
	INNER JOIN
	(
		SELECT MarkOrd_Id
			,NetWeight=SUM(NetWeight)
			,AllWeight=SUM(AllWeight)
			,Amount=SUM(1)
		FROM Sys_MarkOrdInfo 
		WHERE MarkOrd_Id=@Ord_Id AND State=1
		GROUP BY MarkOrd_Id
	)AS B ON B.MarkOrd_Id=A.MarkOrd_Id
	WHERE A.MarkOrd_Id=@Ord_Id

	SELECT AllWeight,
		UpdateFields='AllWeight'
	FROM Sys_MarkOrdInfo
	WHERE MarkOrdInfo_Id=@Key_Id

	SELECT A.NetWeight,AllWeight,Amount
		,UpdateFieldsOrd='NetWeight,AllWeight,Amount'
	FROM Sys_MarkOrd A
	WHERE A.MarkOrd_Id=@Ord_Id 

7、删除子表单行
	UPDATE Sys_OrdersInfoNew SET
		State=0	
	FROM Sys_OrdersInfoNew A
	WHERE OrdInfo_Id=@Key_Id

	EXEC [Sys_Orders_Add_Edit_Del_UPDATE_ORDERNew] @Ord_Id,-1

	SELECT A.Total_Amount
	,UpdateFieldsOrd='Total_Amount'
	FROM Sys_OrdersNew A
	WHERE A.Ord_Id=@Ord_Id

8、删除子表多行
	UPDATE Sys_OrdersInfoNew SET
		State=0	
	FROM Sys_OrdersInfoNew A
	INNER JOIN StringSplitToTable(@Key_Ids,',') AS C ON A.OrdInfo_Id=C.Id

	EXEC [Sys_Orders_Add_Edit_Del_UPDATE_ORDERNew] @Ord_Id,-1

	SELECT A.Total_Amount
	,UpdateFieldsOrd='Total_Amount'
	FROM Sys_OrdersNew A
	WHERE A.Ord_Id=@Ord_Id

9、打印BarTender标签
	--除主键MarkOrdInfo_Id外，字段都传入BarTender模板
	SELECT A.MarkOrdInfo_Id,A.Barcode,A.NetWeight,A.AllWeight
      ,B.Name,PC.SetText AS PqName --StylePic,Pic_Version,--如果要打印图片需返回
	FROM Sys_MarkOrdInfo AS A
	LEFT OUTER JOIN [V_SYS_PM] B ON B.Pm_Id=A.Pm_Id
	INNER JOIN [dbo].[StringSplitToTable](@Key_Ids,',')AS Ids ON Ids.Id=A.MarkOrdInfo_Id
	LEFT OUTER JOIN Bse_Set PC ON PC.SetValue=A.Pq_Id AND PC.SetKey='PmQuality'
	WHERE A.State=1

10、批量修改
	SET @EXEC_SQL = 'UPDATE Sys_OrdersInfoNew SET ' + @strEditSql
	SET @EXEC_SQL = @EXEC_SQL + ' WHERE OrdInfo_Id IN (' + CAST(@Key_Ids AS VARCHAR)+')'
	EXEC (@EXEC_SQL)

	--返回修改后数据源，需要与获取对应明细数据源相同架构
	SELECT  
	 A.* 
	  ,B.StylePic
	  ,B.Pic_Version
	  ,B.PNumber
	  ,B.Name
	FROM Sys_OrdersInfoNew  A
	INNER JOIN V_SYS_PM B ON B.Pm_Id=A.Pm_Id
 	INNER JOIN StringSplitToTable(@Key_Ids,',') AS C ON A.OrdInfo_Id=C.Id

11、Excel导入

12、明细拆分
	DECLARE @Split Table 
	( 
		OrdMInfo_Id		DECIMAL(18,0),
		Amount			INT,
		FySrc			varchar(20),
		Remark			varchar(500)
	)
	BEGIN TRY 
		BEGIN TRAN
			INSERT INTO @Split EXEC(@strSplits)--------创建明细临时表

			INSERT INTO Sys_OrdersMInfo
			(OrdMInfo_IdP,OrdInfo_Id,Ord_Id,[Pm_Id],Pm_ComId,[Weight],[Amount],[Total_Weight],Remark,FySrc)
			SELECT A.OrdMInfo_Id,O.OrdInfo_Id,O.Ord_Id,O.Pm_Id,O.Pm_ComId,O.Weight,A.Amount
				,O.Weight*A.Amount,A.Remark,A.FySrc
			FROM @Split A 
			INNER JOIN Sys_OrdersMInfo O ON O.OrdMInfo_Id=A.OrdMInfo_Id
			WHERE
			O.OrdMInfo_Id=@Key_Id

			UPDATE Sys_OrdersMInfo SET
			IsParent=1
			WHERE OrdMInfo_Id=@Key_Id

			SELECT A.*
			  ,B.StylePic
			  ,B.Pic_Version
			  ,B.PName
			  ,B.[Code]
			FROM Sys_OrdersMInfo A
			INNER JOIN [V_SYS_PM_ITEM] B ON B.Pm_ComId=A.Pm_ComId
			WHERE
			A.Ord_Id=@Ord_Id AND A.IsParent=0	

			SELECT DSNME='gridVInfo'			
		COMMIT TRAN
	END TRY 
	BEGIN CATCH 
		IF (@@ERROR<>0)
			BEGIN
				ROLLBACK TRAN
				SELECT ERROR=ERROR_MESSAGE()
			END
	END CATCH

13、批量新增
	DECLARE @SplitM Table 
	( 
		Pm_Id			DECIMAL(18,0),
		Amount			INT,
		Remark			varchar(500)
	)
	BEGIN TRY 
		BEGIN TRAN
			INSERT INTO @SplitM EXEC(@strSplits)--------创建明细临时表

			INSERT INTO Sys_OrdersInfo
			(Ord_Id,[Pm_Id],Pm_ComId,IsAllSale,[Weight],[Amount],[Total_Weight],[Remark]
			,CUserId,NoOutAmount,NoOutTotal_Weight,Total_AmountCpNotIn,Total_WeightCpNotIn)
			SELECT @Ord_Id,O.Pm_Id,O.Pm_ComId,1,O.Weight,A.Amount,O.Weight*A.Amount,A.Remark
				,@EUser_Id,A.Amount,O.Weight*A.Amount,A.Amount,O.Weight*A.Amount
			FROM @SplitM A 
			INNER JOIN [V_SYS_PM_ITEM] O ON O.[Pm_Id]=A.[Pm_Id]
			WHERE
			O.[PMState]=1 AND O.[ComState]=1 AND O.[IsAllSale]=1

			EXEC [Sys_Orders_Add_Edit_Del_UPDATE_ORDER]@Ord_Id,-1

			SELECT A.Total_Amount,A.Total_Weight,UpdateFieldsOrd='Total_Amount,Total_Weight'
			FROM Sys_Orders A
			WHERE A.Ord_Id=@Ord_Id

			SELECT A.*
			  ,B.StylePic
			  ,B.Pic_Version
			  ,B.PName
			  ,B.[Code]
			FROM Sys_OrdersInfo A 
			INNER JOIN [V_SYS_PM_ITEM] B ON B.Pm_ComId=A.Pm_ComId
			WHERE
			A.Ord_Id=@Ord_Id AND A.State=1	

			SELECT DSNME='gridVMain,gridVInfo'			
		COMMIT TRAN
	END TRY 
	BEGIN CATCH 
		IF (@@ERROR<>0)
			BEGIN
				ROLLBACK TRAN
				SELECT ERROR=ERROR_MESSAGE()
			END
	END CATCH


基础编辑页面
1、新增主表	
	set @sql = 'INSERT INTO Sys_ProductInfo(' + @strFields + ') VALUES(' + @strFieldValues + ')' + ';SELECT @idA=@@identity' 
	exec sp_executesql @sql,N'@idA decimal output',@id output

	UPDATE Sys_ProductInfo SET
	StylePic='YHSYBMod-'+SUBSTRING(CONVERT(varchar(12), @id+100000000000),2,12)+'.jpg'
	,ExistFile=@UImg
	FROM Sys_ProductInfo A
	WHERE A.[Pm_Id]=@id

	SELECT [Pm_Id],StylePic,Pic_Version,State,ComAmount
		,UpdateFields='StylePic,Pic_Version,State,ComAmount'
	FROM Sys_ProductInfo A
	WHERE [Pm_Id]=@id

2、修改主表
	IF(@strEditSql<>'')
	BEGIN
		SET @EXEC_SQL = 'UPDATE Sys_ProductInfo SET ' + @strEditSql
		SET @EXEC_SQL = @EXEC_SQL + ' WHERE Pm_Id=' + CAST(@Key_Id AS VARCHAR)
		EXEC (@EXEC_SQL)
	END
	IF(@UImg=1)
	BEGIN
		UPDATE Sys_ProductInfo SET
			Pic_Version=CASE WHEN ExistFile=1 THEN Pic_Version+1 ELSE 1 END,ExistFile=1
		WHERE [Pm_Id]=@Key_Id
	END

	SELECT Pic_Version
		,UpdateFields='Pic_Version'
	FROM Sys_ProductInfo A
	WHERE [Pm_Id]=@Key_Id

3、弃用/启用
	UPDATE Sys_ProductInfo SET
		State=@State
	FROM Sys_ProductInfo
	WHERE [Pm_Id]=@Key_Id

4、新增子表
	set @sql = 'INSERT INTO Sys_ProductInfoCom(' + @strFields + ') VALUES(' + @strFieldValues + ')' + ';SELECT @idA=@@identity' 
	exec sp_executesql @sql,N'@idA decimal output',@id output

	EXEC [Sys_ProductInfo_Update_ComAmount]@Ord_Id

	INSERT INTO Sys_ProductInfoOffer(Pm_Id,Pm_ComId)
	SELECT Pm_Id,Pm_ComId
	FROM Sys_ProductInfoCom
	WHERE Pm_ComId=@id

	SELECT Pm_ComId=@id

	SELECT ComAmount
		,UpdateFieldsOrd='ComAmount'
	FROM Sys_ProductInfo
	WHERE Pm_Id=@Ord_Id

5、修改子表
	SET @EXEC_SQL = 'UPDATE Sys_ProductInfoCom SET ' + @strEditSql
	SET @EXEC_SQL = @EXEC_SQL + ' WHERE Pm_ComId=' + CAST(@Key_Id AS VARCHAR)
	EXEC (@EXEC_SQL)

6、批量修改
	SET @EXEC_SQL = 'UPDATE Sys_OrdersInfoNew SET ' + @strEditSql
	SET @EXEC_SQL = @EXEC_SQL + ' WHERE OrdInfo_Id IN (' + CAST(@Key_Ids AS VARCHAR)+')'
	EXEC (@EXEC_SQL)

	--返回修改后数据源，需要与获取对应明细数据源相同架构
	SELECT  
	 A.* 
	  ,B.StylePic
	  ,B.Pic_Version
	  ,B.PNumber
	  ,B.Name
	FROM Sys_OrdersInfoNew  A
	INNER JOIN V_SYS_PM B ON B.Pm_Id=A.Pm_Id
 	INNER JOIN StringSplitToTable(@Key_Ids,',') AS C ON A.OrdInfo_Id=C.Id


基础维护页面
1、查询
	SELECT A.*
	FROM Bse_SetOrd A 

2、新增
	set @sql = 'INSERT INTO Bse_SetOrd (' + @strFields + ') VALUES(' + @strFieldValues+')' + ';SELECT @idA=@@identity' 
	exec sp_executesql @sql,N'@idA decimal output',@id output

	SELECT SetOrder=@id

3、修改
	SET @EXEC_SQL = 'UPDATE Bse_SetOrd SET ' + @strEditSql
	SET @EXEC_SQL = @EXEC_SQL + ' WHERE SetOrder=' + CAST(@Key_Id AS VARCHAR)
	EXEC (@EXEC_SQL) 

4、删除
	DELETE Bse_Set
	FROM Bse_Set A
	INNER JOIN Bse_SetOrd B ON B.SetKey=A.SetKey
	WHERE B.SetOrder=@Key_Id

	DELETE Bse_SetOrd
	FROM Bse_SetOrd A
	WHERE A.SetOrder=@Key_Id

5、批量删除
	DELETE Bse_Set
	FROM Bse_Set A
	INNER JOIN Bse_SetOrd B ON B.SetKey=A.SetKey
 	INNER JOIN StringSplitToTable(@Key_Ids,',') AS C ON B.SetOrder=C.Id

	DELETE Bse_SetOrd
	FROM Bse_SetOrd A
 	INNER JOIN StringSplitToTable(@Key_Ids,',') AS C ON A.SetOrder=C.Id

6、批量修改
	SET @EXEC_SQL = 'UPDATE Sys_OrdersInfoNew SET ' + @strEditSql
	SET @EXEC_SQL = @EXEC_SQL + ' WHERE OrdInfo_Id IN (' + CAST(@Key_Ids AS VARCHAR)+')'
	EXEC (@EXEC_SQL)

	--返回修改后数据源，需要与获取对应明细数据源相同架构
	SELECT  
	 A.* 
	  ,B.StylePic
	  ,B.Pic_Version
	  ,B.PNumber
	  ,B.Name
	FROM Sys_OrdersInfoNew  A
	INNER JOIN V_SYS_PM B ON B.Pm_Id=A.Pm_Id
 	INNER JOIN StringSplitToTable(@Key_Ids,',') AS C ON A.OrdInfo_Id=C.Id

报表呈现页面
	SELECT B.PBankName,D.Amount,D.Total_Weight AS NetWeight,E.PName,E.Weight INTO #TEMP
	FROM Sys_Orders A 
	LEFT JOIN Bse_Bank B ON  A.Bank_Id=B.Bank_Id
	INNER JOIN Sys_OrdersInfo D ON A.Ord_Id=D.Ord_Id
	INNER JOIN [V_SYS_PM_ITEM] E ON E.[Pm_ComId]=D.Pm_ComId
	WHERE 
	(@OrdDateSt='' OR A.OrdDate>=@OrdDateSt)
    AND (@OrdDateEd='' OR A.OrdDate<=@OrdDateEd)
	AND D.State=1 AND A.OrdState<>0 
	ORDER BY B.Prov_Id,B.PBankName

	SELECT *
	FROM #TEMP

	SELECT *
	FROM #TEMP

	SELECT DSNME='tOrd-dsOrd,tOrd2-dsOrd'

树形维护模板
查询	
	SELECT A.*
	FROM Sys_PropKind A 
--	WHERE 
--	(A.Fy_Id=-1 OR A.Fy_Id=@Fy_Id)
	ORDER BY Kind_Level ASC,State DESC,Kind_Id ASC
	
	RETURN
新增
	set @sql = 'INSERT INTO Sys_PropKind(' + @strFields + ',Fy_Id) VALUES(' + @strFieldValues + ','+CAST(@Fy_Id AS VARCHAR)+')' + ';SELECT @idA=@@identity' 
	exec sp_executesql @sql,N'@idA decimal output',@id output

	SELECT Kind_Id=@id
修改
	SET @EXEC_SQL = 'UPDATE Sys_PropKind SET ' + @strEditSql
	SET @EXEC_SQL = @EXEC_SQL + ' WHERE Kind_Id=' + CAST(@Key_Id AS VARCHAR)
	EXEC (@EXEC_SQL)




