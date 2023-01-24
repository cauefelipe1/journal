import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:journal_mobile_app/application.dart';
import 'package:journal_mobile_app/features/vehicle/vehicle_repository.dart';
import 'package:journal_mobile_app/gui/components/my_vehicles/my_vehicle_card_component.dart';
import 'package:journal_mobile_app/l10n/app_localization_context.dart';
import 'package:journal_mobile_app/models/vehicle.dart';
import 'package:journal_mobile_app/routes/routes_constants.dart';

var driverVehiclesProvider = FutureProvider.autoDispose((ref) {
  final userInfo = ref.watch(loggedUserInfoProvider).value;
  return ref.watch(vehicleRepositoryProvider).getDriverVehicles(userInfo!.userId);
});

var currentVehicleIdProvider = StateProvider<int?>((ref) => null);

class MyVehiclesComponent extends ConsumerStatefulWidget {
  final ValueChanged<int>? onCardPressed;
  final ValueChanged<int>? onDataIsReady;
  final Function(String)? onNavigateRequest;

  MyVehiclesComponent({
    Key? key,
    this.onCardPressed,
    this.onDataIsReady,
    this.onNavigateRequest,
  }) : super(key: key);

  @override
  ConsumerState<ConsumerStatefulWidget> createState() {
    return _MyVehiclesComponentState(
      onCardPressed: onCardPressed,
      onDataIsReady: onDataIsReady,
    );
  }
}

class _MyVehiclesComponentState extends ConsumerState<MyVehiclesComponent> {
  final ValueChanged<int>? onCardPressed;
  final ValueChanged<int>? onDataIsReady;

  bool isCreated = false;
  int? currentVheicle = null;

  _MyVehiclesComponentState({this.onCardPressed, this.onDataIsReady});

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: 190,
      child: Column(
        children: [
          Row(
            children: [
              Text(
                context.l10n.myVehiclesText,
                style: const TextStyle(
                  color: Colors.black,
                  fontSize: 20,
                  fontWeight: FontWeight.bold,
                ),
              ),
              const Spacer(),
              TextButton(
                onPressed: () => widget.onNavigateRequest?.call(RoutesConstants.newVehicle),
                child: Text(
                  context.l10n.newText,
                  style: const TextStyle(
                    color: Colors.teal,
                    fontSize: 15,
                    fontWeight: FontWeight.bold,
                  ),
                ),
              )
            ],
          ),
          Expanded(
            child: Consumer(
              builder: (context, ref, child) {
                return ref.watch(driverVehiclesProvider).when(
                      data: _getComponentBody,
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

  Widget _getComponentBody(List<VehicleModel> vehicles) {
    if (vehicles.length > 0) {
      _scheduleOnDataReadyEvent(vehicles);
      return _getListOrCard(vehicles);
    } else {
      return _getNoVehicleBody();
    }
  }

  Widget _getNoVehicleBody() {
    //TODO: Implement a proper body.
    return Text("You don't have any vehicle. Lets create one?");
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
              vehicleId: vehicle.id!,
              type: vehicle.type!,
              name: vehicle.displayName,
              onPressed: onCardPressed,
            );
          });
    }

    final vehicle = vehicles[0];

    return MyVehicleCardComponent(
      vehicleId: vehicle.id!,
      name: vehicle.displayName,
      type: vehicle.type!,
      width: double.infinity,
      onPressed: onCardPressed,
    );
  }

  void _scheduleOnDataReadyEvent(List<VehicleModel> vehicles) {
    if (!isCreated) {
      if (vehicles.length <= 0) {
        //If there is no vehicles, no need to even schedule the post frame callback.
        isCreated = true;
        return;
      }

      currentVheicle = vehicles[0].id!;

      WidgetsBinding.instance.addPostFrameCallback((_) {
        if (!isCreated && currentVheicle != null) {
          isCreated = true;

          onDataIsReady?.call(currentVheicle!);
        }
      });
    }
  }
}
