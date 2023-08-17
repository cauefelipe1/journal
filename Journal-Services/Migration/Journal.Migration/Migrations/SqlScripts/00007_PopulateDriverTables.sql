DO $$
    DECLARE
        l_driver_id BIGINT;
    BEGIN
        /* BEGIN: Driver 1 */
        insert into $(Schema).driver
            (first_name, last_name, country_id, user_id)
        values
            ('Dominic', 'Toretto', 224, (select secondary_id from $(Identity_Schema).app_user where user_name like 'dominic'))
        returning driver_id into l_driver_id;

        insert into $(Schema).vehicle
        (secondary_id, model, nickname, vehicle_type_id, vehicle_brand_id, model_year, main_driver_id)
        values
            (null, 'RX7', null, 1, 5 /*Mazda*/, 1997, 1),
            (null, 'Charger', null, 1, 7 /*Dodge*/, 1970, 1);
        /* END: Driver 1 */

        /* BEGIN: Driver 2 */
        insert into $(Schema).driver
        (first_name, last_name, country_id, user_id)
        values
            ('Brian', 'O''Conner', 224, (select secondary_id from $(Identity_Schema).app_user where user_name like 'brian'))
        returning driver_id into l_driver_id;

        insert into $(Schema).vehicle
        (secondary_id, model, nickname, vehicle_type_id, vehicle_brand_id, model_year, main_driver_id)
        values
            (null, 'Supra', null, 1, 6 /*Toyota*/, 1997, 2),
            (null, 'Skyline', null, 1, 8 /*Nissan*/, 1999, 2);
        /* END: Driver 2 */

    END $$