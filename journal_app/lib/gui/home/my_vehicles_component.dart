import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:journal_mobile_app/features/vehicle/vehicle_repository.dart';
import 'package:journal_mobile_app/gui/home/my_vehicle_card_component.dart';
import 'package:journal_mobile_app/models/vehicle.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';

var myVehiclesProvider = FutureProvider.autoDispose((ref) => VehicleRepository().getDriverVehicles(2));

class MyVehiclesComponent extends StatelessWidget {
  const MyVehiclesComponent({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    var l10n = AppLocalizations.of(context)!;

    return SizedBox(
      height: 190,
      child: Column(
        children: [
          Row(
            children: [
              Text(
                l10n.myVehiclesText,
                style: const TextStyle(
                  color: Colors.black,
                  fontSize: 20,
                  fontWeight: FontWeight.bold,
                ),
              ),
              const Spacer(),
              Text(
                l10n.seeAllText,
                style: const TextStyle(
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
            child: Consumer(
              builder: (context, ref, child) {
                return ref.watch(myVehiclesProvider).when(
                      data: _getListOrCard,
                      loading: () => const Center(child: CircularProgressIndicator()),
                      error: (error, stackTrace) => Text(error.toString()),
                    );
              },
            ),
          ),
        ],
      ),
    );
  }

  Widget _getListOrCard(List<VehicleModel> vehicles) {
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
