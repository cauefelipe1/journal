import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:journal_mobile_app/features/vehicle/vehicle_repository.dart';
import 'package:journal_mobile_app/gui/home/my_vehicle_card_component.dart';
import 'package:journal_mobile_app/models/vehicle.dart';

var myVehiclesProvider = FutureProvider.autoDispose((ref) => VehicleRepository().getDriverVehicles(2));

class MyVehiclesComponent extends ConsumerWidget {
  const MyVehiclesComponent({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    var vehicles = ref.watch(myVehiclesProvider);

    return vehicles.when(
      data: (data) => _getBody(context, ref, data),
      loading: () => const CircularProgressIndicator(),
      error: (error, stackTrace) => Text(error.toString()),
    );
  }

  Widget _getBody(BuildContext context, WidgetRef ref, List<VehicleModel> vehicles) {
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
            child: _getListOrCard(context, ref, vehicles),
          ),
        ],
      ),
    );
  }

  Widget _getListOrCard(BuildContext context, WidgetRef ref, List<VehicleModel> vehicles) {
    if (vehicles.length > 1) {
      return ListView.builder(
          scrollDirection: Axis.horizontal,
          itemCount: vehicles.length,
          itemExtent: 160,
          itemBuilder: (context, index) {
            final vehicle = vehicles[index];

            return MyVehicleCardComponent(
              name: vehicle.displayName,
              type: vehicle.type!,
            );
          });
    }

    final vehicle = vehicles[0];

    return MyVehicleCardComponent(
      name: vehicle.displayName,
      type: vehicle.type!,
      width: double.infinity,
    );
  }
}
