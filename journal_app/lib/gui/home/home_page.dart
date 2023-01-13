import 'package:flutter/material.dart';
import 'package:journal_mobile_app/gui/components/my_vehicles/my_vehicles_component.dart';

import 'package:journal_mobile_app/gui/components/vehicle_history/vehicle_history_component.dart';
import 'package:journal_mobile_app/gui/home/welcome_component.dart';
import 'package:journal_mobile_app/gui/vehicle/new_vehicle_page.dart';

class HomePage extends StatefulWidget {
  @override
  State<StatefulWidget> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  int currentVehicle = 0;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.grey[200],
      body: Column(
        children: [
          const WelcomeComponent(),
          Padding(
            padding: EdgeInsets.all(15),
            child: MyVehiclesComponent(
              onCardPressed: (vehicleId) => setState(() => currentVehicle = vehicleId),
            ),
          ),
          ElevatedButton(
            onPressed: () => {
              Navigator.of(context).push(MaterialPageRoute(builder: (context) => const NewVehiclePage())),
            },
            child: const Text(
              "New vehicle",
              style: TextStyle(
                fontSize: 20,
              ),
            ),
          ),
          VehicleHistoryComponent(
            vehicleId: currentVehicle,
          ),
        ],
      ),
    );
  }
}
