CREATE TABLE public.s01_config (
	"ＩＤ" serial NOT NULL,
	項目 varchar(50) NOT NULL,
	数値 numeric(12) NULL,
	テキスト varchar(100) NULL,
	CONSTRAINT s01_config_pkey PRIMARY KEY ("ＩＤ", "項目")
);
