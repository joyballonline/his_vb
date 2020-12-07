CREATE TABLE public.l11_aclog (
	マシン名 varchar(50) NOT NULL,
	初回アクセス日時 timestamp NULL,
	access_timestamp timestamp NULL,
	CONSTRAINT l11_aclog_pkey PRIMARY KEY ("マシン名")
);
