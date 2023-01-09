import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:journal_mobile_app/features/vehicle_event/vehicle_event_repository.dart';
import 'package:journal_mobile_app/l10n/app_localization_context.dart';
import 'package:journal_mobile_app/models/vehicle_event.dart';

var vehicleEventsProvider = FutureProvider.autoDispose((ref) {
  //TODO: Get from the vehicle selected, so the component should receive it by a parameter
  return ref.watch(vehicleEventsRepositoryProvider).getVehicleEvents(3);
});

class VehicleHistoryComponent extends StatelessWidget {
  const VehicleHistoryComponent({super.key});

  @override
  Widget build(BuildContext context) {
    return Expanded(
      child: Padding(
        padding: const EdgeInsets.all(15),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(
              context.l10n.history,
              style: const TextStyle(
                color: Colors.black,
                fontSize: 20,
                fontWeight: FontWeight.bold,
              ),
            ),
            Consumer(
              builder: (context, ref, child) {
                return ref.watch(vehicleEventsProvider).when(
                      data: _getComponentBody,
                      loading: () => const Center(child: CircularProgressIndicator()),
                      error: (error, stackTrace) => Text(error.toString()),
                    );
              },
            ),
          ],
        ),
      ),
    );
  }

  Widget _getComponentBody(List<VehicleEventModel> events) {
    return Expanded(
      child: ListView.builder(
          padding: EdgeInsets.symmetric(horizontal: 5),
          itemCount: events.length,
          itemExtent: 50,
          itemBuilder: (context, index) {
            final event = events[index];

            return Row(
              children: [
                Text(event.type.toString()),
                SizedBox(width: 10),
                Text(event.description!),
                SizedBox(width: 10),
                Text(event.vehicleOdometer.toString()),
              ],
            );
          }),
    );
  }
}
