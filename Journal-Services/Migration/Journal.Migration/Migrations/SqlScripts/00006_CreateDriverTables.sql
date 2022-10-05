DO $$
DECLARE
    l_driver_id INTEGER;
BEGIN

    /* BEGIN: Driver 1 */
    insert into $(Schema).driver
        (first_name, last_name, user_id)
    values
        ('Dominic', 'Toretto', 1)
    returning driver_id into l_driver_id;

    insert into $(Schema).vehicle
        (vehicle_secondary_id, model, nickname, vehicle_type_id, vehicle_brand_id, model_year, main_driver_id)
    values
        (null, 'RX7', null, 1, 5 /*Mazda*/, 1997, l_driver_id),
        (null, 'Charger', null, 1, 7 /*Dodge*/, 1970, l_driver_id);
    /* END: Driver 1 */

    /* BEGIN: Driver 2 */
    insert into $(Schema).driver
    (first_name, last_name, user_id)
    values
        ('Brian', 'O''Conner', 2)
        returning driver_id into l_driver_id;

    insert into $(Schema).vehicle
    (vehicle_secondary_id, model, nickname, vehicle_type_id, vehicle_brand_id, model_year, main_driver_id)
    values
        (null, 'Supra', null, 1, 6 /*Toyota*/, 1997, l_driver_id),
        (null, 'Skyline', null, 1, 8 /*Nissan*/, 1999, l_driver_id);
    /* END: Driver 2 */

END $$