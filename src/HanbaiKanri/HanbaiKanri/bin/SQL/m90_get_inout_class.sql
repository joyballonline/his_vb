CREATE OR REPLACE FUNCTION public.m90_get_inout_class(a_ character varying, b_ character varying, c_ character varying)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
declare w1 varchar(100);
w2 varchar(100);
	begin
		
SELECT 文字１,文字２ into w1,w2 FROM m90_hanyo WHERE 会社コード = a_ AND 固定キー = b_ AND 可変キー = c_;
		return w2;
	END;
$function$
;
