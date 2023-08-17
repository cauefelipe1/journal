DO $$
BEGIN

    INSERT INTO $(Identity_Schema).app_user
        (id, access_failed_count, concurrency_stamp, email, email_confirmed, lockout_enabled, lockout_end, normalized_email, normalized_user_name, password_hash, phone_number, phone_number_confirmed, security_stamp, two_factor_enabled, user_name)
    VALUES
        ('babca1b6-7c9a-49fd-8893-73319f5d970f', 0, '6749eacd-c68e-4044-bd07-35c5f194ca23', 'dominic@server.com', false, true, NULL, 'DOMINIC@SERVER.COM', 'DOMINIC', 'AQAAAAEAACcQAAAAEIltXxVRGRuRp46/a5qRBT3IjwHNbAocroyAtEq42T6DZoSfQtABFzju0yRL/YRFxQ==', NULL, false, 'A6VUOY7SGE6GJEZ3QNNFKC5IWEQLOXLG', false, 'dominic');

    INSERT INTO $(Identity_Schema).app_user
        (id, access_failed_count, concurrency_stamp, email, email_confirmed, lockout_enabled, lockout_end, normalized_email, normalized_user_name, password_hash, phone_number, phone_number_confirmed, security_stamp, two_factor_enabled, user_name)
    VALUES
        ('99e98426-48af-440c-9db6-b36cc1d6e8cc', 0, '580f1977-2a0d-45a7-b419-1d5df40e1bbb', 'brian@server.com', false, true, NULL, 'BRIAN@SERVER.COM', 'BRIAN', 'AQAAAAEAACcQAAAAEE1u8vqTr8re21m0aIawcpRow6UzeqR68TdAASiV2HM3lU2iGlvCZ3j8WmN4YhBWkA==', NULL, false, 'AC5XJ5ULTGJU3Q6PSLPNJHBRE4I3RLSX', false, 'brian');

    INSERT INTO $(Identity_Schema).app_user
        (id, access_failed_count, concurrency_stamp, email, email_confirmed, lockout_enabled, lockout_end, normalized_email, normalized_user_name, password_hash, phone_number, phone_number_confirmed, security_stamp, two_factor_enabled, user_name)
    VALUES
        ('93287bcb-090d-4f4b-adfa-ba792f78377f', 0, 'b5c6ecf1-c3ff-4ba9-b599-0e2452ece698', 'joe_biden@server.com', false, true, NULL, 'JOE_BIDEN@SERVER.COM', 'JOE_BIDEN', 'AQAAAAEAACcQAAAAEMbGwdW1xIL+DMC4Lf0StmSdDYgv6a2P99jsIFoZiwXJYz9Bw2QO09oJdNBDhtGVlg==', NULL, false, 'CHCBOMVFRQWTPMXF5KI3V73VA5JGHSTI', false, 'joe_biden');

END $$
