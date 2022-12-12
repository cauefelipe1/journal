import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:journal_mobile_app/gui/home/my_vehicle_card_component.dart';

class MyVehiclesComponent extends ConsumerWidget {
  const MyVehiclesComponent({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return _getBody(context, ref);
  }

  Widget _getBody(BuildContext context, WidgetRef ref) {
    return SizedBox(
      height: 190,
      child: Column(
        children: [
          Row(
            children: const [
              Text(
                "My Vehicles", //TODO: Translate
                style: TextStyle(
                  color: Colors.black,
                  fontSize: 20,
                  fontWeight: FontWeight.bold,
                ),
              ),
              Spacer(),
              Text(
                "See all", //TODO: Translate
                style: TextStyle(
                  color: Colors.teal,
                  fontSize: 15,
                  fontWeight: FontWeight.bold,
                ),
              )
            ],
          ),
          const SizedBox(
            height: 10,
          ),
          Expanded(
            child: ListView(
              scrollDirection: Axis.horizontal,
              children: const [
                MyVehicleCardComponent(name: "Skyline", type: 1),
                MyVehicleCardComponent(name: "Mustang", type: 1),
                MyVehicleCardComponent(name: "Corsa", type: 1),
                MyVehicleCardComponent(name: "Vectra", type: 1),
                MyVehicleCardComponent(name: "Viper", type: 1),
                MyVehicleCardComponent(name: "RX 7", type: 1),
                MyVehicleCardComponent(name: "Eclipse", type: 1),
                MyVehicleCardComponent(name: "Supra", type: 1),
              ],
            ),
          )
        ],
      ),
    );
  }
}
