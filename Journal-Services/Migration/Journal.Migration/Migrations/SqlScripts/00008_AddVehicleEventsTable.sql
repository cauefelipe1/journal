DO $$
BEGIN
    insert into $(Schema).vehicle_event_type
        (vehicle_event_type_id, vehicle_event_type_desc)
    values
        (1, 'Refueling'),
        (2, 'Maintenance'),
        (3, 'Route'),
        (4, 'Expense'),
        (5, 'Income');
END $$