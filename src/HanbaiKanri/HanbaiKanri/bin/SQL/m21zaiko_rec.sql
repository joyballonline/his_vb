DECLARE w入出庫区分 char(1);
        w数量 numeric;
        w仕入値 numeric;
		wInventoryControl char(1);
		wRECYesNo char(1);
		
BEGIN
    -- INSERT 在庫管理区分の取得
    -- SELECT 在庫管理区分 INTO wInventoryControl FROM M01_COMPANY WHERE 会社コード = NEW.会社コード; 
	--   wInventoryControl := '3';
	IF (NEW.入出庫区分 = '1') THEN
		wInventoryControl := 'V';
	ELSE
		wInventoryControl := '3';
	END IF;

	-- 仕入原価を取得しておく
	w仕入値 := 0;
    SELECT 仕入値 INTO w仕入値 FROM t43_nyukodt WHERE 会社コード = NEW.会社コード AND 入庫番号 = NEW.伝票番号 AND 行番号 = NEW.行番号;

	-- RAISE NOTICE  'NOTICE RAISE MESSAGE %', wInventoryControl;

	-- 在庫マスタに処理対象となる在庫レコードが存在するかしないかを在庫管理区分状態に応じてチェックする
	-- m21_zaiko のキー項目がwInventoryControl設定と矛盾し、nullがセットされることを前提としてみた
	CASE wInventoryControl
		 WHEN '0' THEN IF EXISTS (SELECT * FROM m21_zaiko 
								   WHERE (CASE WHEN NEW.会社コード is null THEN 会社コード is null ELSE 会社コード= NEW.会社コード END)
				 		             AND (CASE WHEN NEW.メーカー is null   THEN メーカー is null   ELSE メーカー = NEW.メーカー END)	
                                     AND (CASE WHEN NEW.品名 is null      THEN 品名 is null      ELSE 品名 = NEW.品名 END)
                                     AND (CASE WHEN NEW.型式 is null      THEN 型式 is null      ELSE 型式 = NEW.型式 END))
					                THEN wRECYesNo := 'N';ELSE wRECYesNo := 'Y';END IF;
									 
		 WHEN '1' THEN IF EXISTS (SELECT * FROM m21_zaiko
								   WHERE (CASE WHEN NEW.会社コード is null THEN 会社コード is null ELSE 会社コード= NEW.会社コード END)
				 		             AND (CASE WHEN NEW.メーカー is null   THEN メーカー is null   ELSE メーカー = NEW.メーカー END)	
                                     AND (CASE WHEN NEW.品名 is null      THEN 品名 is null      ELSE 品名 = NEW.品名 END)
                                     AND (CASE WHEN NEW.型式 is null      THEN 型式 is null      ELSE 型式 = NEW.型式 END)
 					  	             AND (CASE WHEN NEW.倉庫コード is null THEN 倉庫コード is null ELSE 倉庫コード = NEW.倉庫コード END))
								    THEN wRECYesNo := 'N';ELSE wRECYesNo := 'Y';END IF;
									
		 WHEN '3' THEN IF EXISTS (SELECT * FROM m21_zaiko
								   WHERE (CASE WHEN NEW.会社コード is null THEN 会社コード is null ELSE 会社コード= NEW.会社コード END)
				 		             AND (CASE WHEN NEW.メーカー is null   THEN メーカー is null   ELSE メーカー = NEW.メーカー END)	
                                     AND (CASE WHEN NEW.品名 is null      THEN 品名 is null      ELSE 品名 = NEW.品名 END)
                                     AND (CASE WHEN NEW.型式 is null      THEN 型式 is null      ELSE 型式 = NEW.型式 END)
 					  	             AND (CASE WHEN NEW.倉庫コード is null THEN 倉庫コード is null ELSE 倉庫コード = NEW.倉庫コード END)
                                     AND (CASE WHEN NEW.入出庫種別 is null THEN 入出庫種別 is null ELSE 入出庫種別 = NEW.入出庫種別 END))
									THEN wRECYesNo := 'N';ELSE wRECYesNo := 'Y';END IF;
									
		 WHEN '7' THEN IF EXISTS (SELECT * FROM m21_zaiko
								   WHERE (CASE WHEN NEW.会社コード is null THEN 会社コード is null ELSE 会社コード= NEW.会社コード END)
				 		             AND (CASE WHEN NEW.メーカー is null   THEN メーカー is null   ELSE メーカー = NEW.メーカー END)	
                                     AND (CASE WHEN NEW.品名 is null      THEN 品名 is null      ELSE 品名 = NEW.品名 END)
                                     AND (CASE WHEN NEW.型式 is null      THEN 型式 is null      ELSE 型式 = NEW.型式 END)
 					  	             AND (CASE WHEN NEW.倉庫コード is null THEN 倉庫コード is null ELSE 倉庫コード = NEW.倉庫コード END)
                                     AND (CASE WHEN NEW.入出庫種別 is null THEN 入出庫種別 is null ELSE 入出庫種別 = NEW.入出庫種別 END)
				                     AND (CASE WHEN NEW.ロケ番号 is null   THEN ロケ番号 is null  ELSE ロケ番号  = NEW.ロケ番号 END))
								    THEN wRECYesNo := 'N';ELSE wRECYesNo := 'Y';END IF;
									
		 WHEN 'F' THEN IF EXISTS (SELECT * FROM m21_zaiko
								   WHERE (CASE WHEN NEW.会社コード is null THEN 会社コード is null ELSE 会社コード= NEW.会社コード END)
				 		             AND (CASE WHEN NEW.メーカー is null   THEN メーカー is null   ELSE メーカー = NEW.メーカー END)	
                                     AND (CASE WHEN NEW.品名 is null      THEN 品名 is null      ELSE 品名 = NEW.品名 END)
                                     AND (CASE WHEN NEW.型式 is null      THEN 型式 is null      ELSE 型式 = NEW.型式 END)
 					  	             AND (CASE WHEN NEW.倉庫コード is null THEN 倉庫コード is null ELSE 倉庫コード= NEW.倉庫コード END)
                                     AND (CASE WHEN NEW.入出庫種別 is null THEN 入出庫種別 is null ELSE 入出庫種別= NEW.入出庫種別 END)
				                     AND (CASE WHEN NEW.ロケ番号 is null   THEN ロケ番号 is null  ELSE ロケ番号  = NEW.ロケ番号 END)
								     AND (CASE WHEN NEW.製造番号 is null   THEN 製造番号 is null  ELSE 製造番号  = NEW.製造番号 END))
							        THEN wRECYesNo := 'N';ELSE wRECYesNo := 'Y';END IF;
		 WHEN 'V' THEN IF EXISTS (SELECT * FROM m21_zaiko
								   WHERE (CASE WHEN NEW.会社コード is null THEN 会社コード is null ELSE 会社コード= NEW.会社コード END)
				 		             AND (CASE WHEN NEW.メーカー is null   THEN メーカー is null   ELSE メーカー = NEW.メーカー END)	
                                     AND (CASE WHEN NEW.品名 is null      THEN 品名 is null      ELSE 品名 = NEW.品名 END)
                                     AND (CASE WHEN NEW.型式 is null      THEN 型式 is null      ELSE 型式 = NEW.型式 END)
 					  	             AND (CASE WHEN NEW.倉庫コード is null THEN 倉庫コード is null ELSE 倉庫コード= NEW.倉庫コード END)
                                     AND (CASE WHEN NEW.入出庫種別 is null THEN 入出庫種別 is null ELSE 入出庫種別= NEW.入出庫種別 END)
				                     AND (CASE WHEN NEW.ロケ番号 is null   THEN ロケ番号 is null  ELSE ロケ番号  = NEW.ロケ番号 END)
								     AND (CASE WHEN NEW.製造番号 is null   THEN 製造番号 is null  ELSE 製造番号  = NEW.製造番号 END)
									 AND (CASE WHEN NEW.伝票番号 is null   THEN 伝票番号 is null  ELSE 伝票番号  = NEW.伝票番号 END)	
                                     AND (CASE WHEN NEW.行番号 is null     THEN 行番号 is null   ELSE 行番号    = NEW.行番号 END)) 
								    THEN wRECYesNo := 'N';ELSE wRECYesNo := 'Y';END IF;
	END CASE;


 	-- RAISE NOTICE  'NOTICE RAISE MESSAGE %,%,%', TG_OP,NEW.入出庫区分,wRecYesNo;
	-- RAISE NOTICE  '会社コード= %',NEW.会社コード;
	-- RAISE NOTICE  'メーカー = % ', NEW.メーカー;	
    -- RAISE NOTICE  '品名 = % ',NEW.品名;
    -- RAISE NOTICE  '型式 = % ',NEW.型式;
 	-- RAISE NOTICE  '倉庫コード= % ',NEW.倉庫コード;
    -- RAISE NOTICE  '入出庫種別= % ',NEW.入出庫種別;
	-- RAISE NOTICE  '伝票番号  = % ',NEW.伝票番号;	
    -- RAISE NOTICE  '行番号    = % ',NEW.行番号;

	-- INSERT トリガに対する処理（t70_inoutにレコード挿入の場合）
	IF (TG_OP = 'INSERT') AND (NEW.入出庫区分 = '1') THEN
		IF wRecYesNo ='Y' THEN				-- 同一キーを持つ在庫マスタが存在しない場合
	       INSERT INTO m21_zaiko
			     (会社コード,倉庫コード,出庫開始サイン,メーカー,品名,型式
		    	 ,製造番号,伝票番号,行番号,入出庫種別
				 ,現在庫数,適正在庫数,発注点数
				 ,入庫単価
				 ,平均単価
				 ,最終仕入単価
				 ,前月末数量,前月末間接費,今月末数量,今月入庫数,今月出庫数,今月間接費
				 ,最終入庫日
				 ,最終出庫日
				 ,最終棚卸日,無効フラグ,更新者,更新日,ロケ番号
				) SELECT
				  NEW.会社コード,NEW.倉庫コード,NEW.出庫開始サイン,NEW.メーカー,NEW.品名,NEW.型式
				 ,NEW.製造番号,NEW.伝票番号,NEW.行番号,NEW.入出庫種別
				 ,NEW.数量,0,0
				 ,w仕入値					-- 入庫:入庫明細仕入値（これ単価？）
				 ,w仕入値					-- 入庫:入庫明細仕入値（これ単価？）
				 ,w仕入値 					-- 入庫:入庫明細仕入値（これ単価？）
				 ,0,0,0,0,0,0
				 ,NEW.入出庫日				-- 入庫:入出庫日
				 ,NULL						-- 入庫:NULL
				 ,NULL,'0',NEW.更新者,NEW.更新日,NEW.ロケ番号
				 ;
				 RETURN NEW;
		ELSE								-- 同一キーを持つ在庫マスタが存在する場合
		 	UPDATE m21_zaiko
			   SET 現在庫数 = 現在庫数 + NEW.数量
			       -- 2020.04.06 平均単価の考え方が間違っているように見えるのでコメントアウト
       			   -- ,m21_zaiko.平均単価 = ((mz.現在庫数 * mz.平均単価) + (t70_inout.数量 * w仕入値))
				  ,最終仕入単価 = CASE WHEN 最終入庫日 < NEW.入出庫日 THEN w仕入値 ELSE 最終仕入単価 END
				  ,最終入庫日 = CASE WHEN 最終入庫日 < NEW.入出庫日 THEN NEW.入出庫日 ELSE 最終入庫日 END
			 WHERE (CASE WHEN NEW.会社コード is null THEN 会社コード is null ELSE 会社コード= NEW.会社コード END)
			   AND (CASE WHEN NEW.メーカー is null   THEN メーカー is null   ELSE メーカー = NEW.メーカー END)	
               AND (CASE WHEN NEW.品名 is null      THEN 品名 is null      ELSE 品名 = NEW.品名 END)	
               AND (CASE WHEN NEW.型式 is null      THEN 型式 is null      ELSE 型式 = NEW.型式 END)	
			   AND (CASE WHEN NEW.倉庫コード is null THEN 倉庫コード is null ELSE 倉庫コード= NEW.倉庫コード END)
               AND (CASE WHEN NEW.入出庫種別 is null THEN 入出庫種別 is null ELSE 入出庫種別= NEW.入出庫種別 END)
               AND (CASE WHEN NEW.ロケ番号 is null   THEN ロケ番号 is null  ELSE ロケ番号  = NEW.ロケ番号 END)
               AND (CASE WHEN NEW.製造番号 is null   THEN 製造番号 is null  ELSE 製造番号  = NEW.製造番号 END)	
               AND (CASE WHEN NEW.伝票番号 is null   THEN 伝票番号 is null  ELSE 伝票番号  = NEW.伝票番号 END)	
			   AND (CASE WHEN NEW.行番号 is null     THEN 行番号 is null   ELSE 行番号    = NEW.行番号 END) 
			       ;
				   RETURN NEW;
        END IF;
	END IF;
	
	IF (TG_OP = 'INSERT') AND (NEW.入出庫区分 = '2') THEN
		IF wRecYesNo ='Y' THEN				-- 同一キーを持つ在庫マスタが存在しない場合
		   -- 本来は論理エラー　在庫品出庫であるにも関わらず、在庫レコード挿入という状態はあり得ない
		   -- 入出庫区分＝１在庫レコード挿入処理と同一ロジックとする
		   -- RAISE NOTICE  'NOTICE RAISE MESSAGE %,%', W入出庫区分,w数量;
	       INSERT INTO m21_zaiko
			     (会社コード,倉庫コード,出庫開始サイン,メーカー,品名,型式
		    	 ,製造番号,伝票番号,行番号,入出庫種別
				 ,現在庫数,適正在庫数,発注点数
				 ,入庫単価
				 ,平均単価
				 ,最終仕入単価
				 ,前月末数量,前月末間接費,今月末数量,今月入庫数,今月出庫数,今月間接費
				 ,最終入庫日
				 ,最終出庫日
				 ,最終棚卸日,無効フラグ,更新者,更新日,ロケ番号
				) SELECT
				  NEW.会社コード,NEW.倉庫コード,NEW.出庫開始サイン,NEW.メーカー,NEW.品名,NEW.型式
				 ,NEW.製造番号,NEW.伝票番号,NEW.行番号,NEW.入出庫種別
				 ,NEW.数量,0,0
				 ,w仕入値					-- 入庫:入庫明細仕入値（これ単価？）
				 ,w仕入値					-- 入庫:入庫明細仕入値（これ単価？）
				 ,w仕入値 					-- 入庫:入庫明細仕入値（これ単価？）
				 ,0,0,0,0,0,0
				 ,NEW.入出庫日				-- 入庫:入出庫日
				 ,NULL						-- 入庫:NULL
				 ,NULL,'0',NEW.更新者,NEW.更新日,NEW.ロケ番号
				 ;
				 RETURN NEW;
	    ELSE								-- 同一キーを持つ在庫マスタが存在する場合
		    -- 出庫処理
		 	UPDATE m21_zaiko
			   SET 現在庫数 = 現在庫数 - NEW.数量
				  ,最終出庫日 = CASE WHEN coalesce(最終出庫日,to_date('20000101','yyyymmdd')) < NEW.入出庫日 THEN NEW.入出庫日 ELSE 最終出庫日 END
			 WHERE (CASE WHEN NEW.会社コード is null THEN 会社コード is null ELSE 会社コード= NEW.会社コード END)
			   AND (CASE WHEN NEW.メーカー is null   THEN メーカー is null   ELSE メーカー = NEW.メーカー END)	
               AND (CASE WHEN NEW.品名 is null      THEN 品名 is null      ELSE 品名 = NEW.品名 END)	
               AND (CASE WHEN NEW.型式 is null      THEN 型式 is null      ELSE 型式 = NEW.型式 END)	
			   AND (CASE WHEN NEW.倉庫コード is null THEN 倉庫コード is null ELSE 倉庫コード= NEW.倉庫コード END)
               AND (CASE WHEN NEW.入出庫種別 is null THEN 入出庫種別 is null ELSE 入出庫種別= NEW.入出庫種別 END)
               AND (CASE WHEN NEW.ロケ番号 is null   THEN ロケ番号 is null  ELSE ロケ番号  = NEW.ロケ番号 END)
               AND (CASE WHEN NEW.製造番号 is null   THEN 製造番号 is null  ELSE 製造番号  = NEW.製造番号 END)
			   AND (CASE WHEN NEW.出庫開始サイン is null THEN CONCAT(伝票番号,行番号) is null ELSE CONCAT(伝票番号,行番号) = NEW.出庫開始サイン END)
			   		-- 出庫開始サインに格納された元伝票番号から在庫マスタを更新	
               --AND (CASE WHEN NEW.伝票番号 is null   THEN 伝票番号 is null  ELSE 伝票番号  = NEW.伝票番号 END)	
			   --AND (CASE WHEN NEW.行番号 is null     THEN 行番号 is null   ELSE 行番号    = NEW.行番号 END) 
			       ;
				   RETURN NEW;
        END IF;
	END IF;

    -- UPDATE トリガに対する処理（t70_inoutにレコード更新の場合→inoutの取消処理を意味する）
	IF (TG_OP = 'UPDATE') AND (NEW.入出庫区分 = '1') THEN
		IF wRecYesNo ='Y' THEN				-- 同一キーを持つ在庫マスタが存在しない場合
		   -- 本来は論理エラー　すでに存在している在庫に対して入庫追加であるにも関わらず、在庫レコード挿入という状態はあり得ない
		   -- 入出庫区分＝１在庫レコード挿入処理と同一ロジックとする
		   -- RAISE NOTICE  'NOTICE RAISE MESSAGE %,%', W入出庫区分,w数量;
	       INSERT INTO m21_zaiko
			     (会社コード,倉庫コード,出庫開始サイン,メーカー,品名,型式
		    	 ,製造番号,伝票番号,行番号,入出庫種別
				 ,現在庫数,適正在庫数,発注点数
				 ,入庫単価
				 ,平均単価
				 ,最終仕入単価
				 ,前月末数量,前月末間接費,今月末数量,今月入庫数,今月出庫数,今月間接費
				 ,最終入庫日
				 ,最終出庫日
				 ,最終棚卸日,無効フラグ,更新者,更新日,ロケ番号
				) SELECT
				  NEW.会社コード,NEW.倉庫コード,NEW.出庫開始サイン,NEW.メーカー,NEW.品名,NEW.型式
				 ,NEW.製造番号,NEW.伝票番号,NEW.行番号,NEW.入出庫種別
				 ,NEW.数量,0,0
				 ,w仕入値					-- 入庫:入庫明細仕入値（これ単価？）
				 ,w仕入値					-- 入庫:入庫明細仕入値（これ単価？）
				 ,w仕入値 					-- 入庫:入庫明細仕入値（これ単価？）
				 ,0,0,0,0,0,0
				 ,NEW.入出庫日				-- 入庫:入出庫日
				 ,NULL						-- 入庫:NULL
				 ,NULL,'0',NEW.更新者,NEW.更新日,NEW.ロケ番号
				 ;
				 RETURN NEW;
	  	ELSE								-- 同一キーを持つ在庫マスタが存在する場合
		 	UPDATE m21_zaiko
			   SET 現在庫数 = 現在庫数 - NEW.数量
			      -- 2020.04.06 平均単価の考え方が間違っているように見えるのでコメントアウト
       			  -- ,mz.平均単価 = CASE t7.入出庫区分 WHEN '1' THEN ((mz.現在庫数 * mz.平均単価) + (t7.数量 * w仕入値)) ELSE mz.平均単価 END
			 WHERE (CASE WHEN NEW.会社コード is null THEN 会社コード is null ELSE 会社コード= NEW.会社コード END)
			   AND (CASE WHEN NEW.メーカー is null   THEN メーカー is null   ELSE メーカー = NEW.メーカー END)	
               AND (CASE WHEN NEW.品名 is null      THEN 品名 is null      ELSE 品名 = NEW.品名 END)	
               AND (CASE WHEN NEW.型式 is null      THEN 型式 is null      ELSE 型式 = NEW.型式 END)	
			   AND (CASE WHEN NEW.倉庫コード is null THEN 倉庫コード is null ELSE 倉庫コード= NEW.倉庫コード END)
               AND (CASE WHEN NEW.入出庫種別 is null THEN 入出庫種別 is null ELSE 入出庫種別= NEW.入出庫種別 END)
               AND (CASE WHEN NEW.ロケ番号 is null   THEN ロケ番号 is null  ELSE ロケ番号  = NEW.ロケ番号 END)
               AND (CASE WHEN NEW.製造番号 is null   THEN 製造番号 is null  ELSE 製造番号  = NEW.製造番号 END)	
               AND (CASE WHEN NEW.伝票番号 is null   THEN 伝票番号 is null  ELSE 伝票番号  = NEW.伝票番号 END)	
			   AND (CASE WHEN NEW.行番号 is null     THEN 行番号 is null   ELSE 行番号    = NEW.行番号 END) 
			       ;
				   RETURN NEW;
    	END IF;
	END IF;
	IF (TG_OP = 'UPDATE') AND (NEW.入出庫区分 = '2') THEN
		IF wRecYesNo ='Y' THEN				-- 同一キーを持つ在庫マスタが存在しない場合
		   -- 本来は論理エラー　すでに存在している在庫に対して入庫追加であるにも関わらず、在庫レコード挿入という状態はあり得ない
		   -- 入出庫区分＝１在庫レコード挿入処理と同一ロジックとする
		   -- RAISE NOTICE  'NOTICE RAISE MESSAGE %,%', W入出庫区分,w数量;
	       INSERT INTO m21_zaiko
			     (会社コード,倉庫コード,出庫開始サイン,メーカー,品名,型式
		    	 ,製造番号,伝票番号,行番号,入出庫種別
				 ,現在庫数,適正在庫数,発注点数
				 ,入庫単価
				 ,平均単価
				 ,最終仕入単価
				 ,前月末数量,前月末間接費,今月末数量,今月入庫数,今月出庫数,今月間接費
				 ,最終入庫日
				 ,最終出庫日
				 ,最終棚卸日,無効フラグ,更新者,更新日,ロケ番号
				) SELECT
				  NEW.会社コード,NEW.倉庫コード,NEW.出庫開始サイン,NEW.メーカー,NEW.品名,NEW.型式
				 ,NEW.製造番号,NEW.伝票番号,NEW.行番号,NEW.入出庫種別
				 ,NEW.数量,0,0
				 ,w仕入値					-- 入庫:入庫明細仕入値（これ単価？）
				 ,w仕入値					-- 入庫:入庫明細仕入値（これ単価？）
				 ,w仕入値 					-- 入庫:入庫明細仕入値（これ単価？）
				 ,0,0,0,0,0,0
				 ,NEW.入出庫日				-- 入庫:入出庫日
				 ,NULL						-- 入庫:NULL
				 ,NULL,'0',NEW.更新者,NEW.更新日,NEW.ロケ番号
				 ;
				 RETURN NEW;
	  	ELSE								-- 同一キーを持つ在庫マスタが存在する場合
		 	UPDATE m21_zaiko
			   SET 現在庫数 = 現在庫数 + NEW.数量
			  -- FROM t70_inout AS t7
			 WHERE (CASE WHEN NEW.会社コード is null THEN 会社コード is null ELSE 会社コード= NEW.会社コード END)
			   AND (CASE WHEN NEW.メーカー is null   THEN メーカー is null   ELSE メーカー = NEW.メーカー END)	
               AND (CASE WHEN NEW.品名 is null      THEN 品名 is null      ELSE 品名 = NEW.品名 END)	
               AND (CASE WHEN NEW.型式 is null      THEN 型式 is null      ELSE 型式 = NEW.型式 END)	
			   AND (CASE WHEN NEW.倉庫コード is null THEN 倉庫コード is null ELSE 倉庫コード= NEW.倉庫コード END)
               AND (CASE WHEN NEW.入出庫種別 is null THEN 入出庫種別 is null ELSE 入出庫種別= NEW.入出庫種別 END)
               AND (CASE WHEN NEW.ロケ番号 is null   THEN ロケ番号 is null  ELSE ロケ番号  = NEW.ロケ番号 END)
               AND (CASE WHEN NEW.製造番号 is null   THEN 製造番号 is null  ELSE 製造番号  = NEW.製造番号 END)
			   AND (CASE WHEN NEW.出庫開始サイン is null THEN CONCAT(伝票番号,行番号) is null ELSE CONCAT(伝票番号,行番号) = NEW.出庫開始サイン END)
			   		-- 出庫開始サインに格納された元伝票番号から在庫マスタを更新	
               --AND (CASE WHEN NEW.伝票番号 is null   THEN 伝票番号 is null  ELSE 伝票番号  = NEW.伝票番号 END)	
			   --AND (CASE WHEN NEW.行番号 is null     THEN 行番号 is null   ELSE 行番号    = NEW.行番号 END) 
			       ;
				   RETURN NEW;
    	END IF;
	END IF;			
 END;
