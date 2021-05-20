ALTER TABLE public.t45_shukodt add column 受注行番号 numeric NULL;

update t45_shukodt set 受注行番号=行番号 where 受注行番号 is null;