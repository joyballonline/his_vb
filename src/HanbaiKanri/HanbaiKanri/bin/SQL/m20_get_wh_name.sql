CREATE OR REPLACE FUNCTION public.m20_get_wh_name(a_ character varying, b_ character varying)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
declare w1 varchar(100);
w2 m20_warehouse.名称%type;
	begin
		
SELECT m20.名称 into w2
           FROM m20_warehouse m20
          WHERE m20.会社コード = a_ AND m20.倉庫コード = b_;

		return w2;
	END;
$function$
;
