CREATE TABLE public.t82_tmp_order(
会社コード varchar(5) default 'ZENBI',
	order_no varchar(50) NULL,
	x varchar(50) NULL
);

ALTER TABLE public.t82_tmp_order ADD 会社コード varchar(5) NULL;
