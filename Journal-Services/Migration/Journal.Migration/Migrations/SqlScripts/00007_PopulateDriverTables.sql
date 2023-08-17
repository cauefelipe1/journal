DO $$
    DECLARE
        l_driver_id BIGINT;
    BEGIN
        /* BEGIN: Driver 1 */
        insert into $(Schema).driver
            (secondary_id, first_name, last_name, country_id, user_id)
        values
            ('c6f65bc3-5b94-4a35-bde5-743dc648c8f2', 'Dominic', 'Toretto', 224, (select secondary_id from $(Identity_Schema).app_user where user_name like 'dominic'))
        returning driver_id into l_driver_id;

        insert into $(Schema).vehicle
            (secondary_id, model, nickname, vehicle_type_id, vehicle_brand_id, model_year, main_driver_id)
        values
            ('770e4907-6119-4758-a542-3c90cf7bc96e', 'RX7', null, 1, 5 /*Mazda*/, 1997, 1),
            ('1ee5f7f6-3bb3-4c7b-b1e6-d9bbbe89f738', 'Charger', null, 1, 7 /*Dodge*/, 1970, 1);
        /* END: Driver 1 */

        /* BEGIN: Driver 2 */
        insert into $(Schema).driver
            (secondary_id, first_name, last_name, country_id, user_id)
        values
            ('4930e98b-2664-4604-940e-3af77fe8c3fe', 'Brian', 'O''Conner', 224, (select secondary_id from $(Identity_Schema).app_user where user_name like 'brian'))
        returning driver_id into l_driver_id;

        insert into $(Schema).vehicle
            (secondary_id, model, nickname, vehicle_type_id, vehicle_brand_id, model_year, main_driver_id)
        values
            ('1fcf10f0-df1f-403d-a302-eb7d46caeac8', 'Supra', null, 1, 6 /*Toyota*/, 1997, 2),
            ('b72ba6be-9fa4-4afd-b4b6-b4aabba2fb52', 'Skyline', null, 1, 8 /*Nissan*/, 1999, 2);
        /* END: Driver 2 */
    END $$