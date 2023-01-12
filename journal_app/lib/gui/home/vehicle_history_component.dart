import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:intl/intl.dart';
import 'package:journal_mobile_app/features/vehicle_event/vehicle_event_repository.dart';
import 'package:journal_mobile_app/l10n/app_localization_context.dart';
import 'package:journal_mobile_app/models/vehicle_event.dart';
import 'package:timeline_tile/timeline_tile.dart';

var vehicleEventsProvider = FutureProvider.autoDispose((ref) {
  //TODO: Get from the vehicle selected, so the component should receive it by a parameter
  return ref.watch(vehicleEventsRepositoryProvider).getVehicleEvents(3);
});

class VehicleHistoryComponent extends StatelessWidget {
  VehicleHistoryComponent({super.key});
  final DateFormat dateFormat = DateFormat.yMd();

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
            SizedBox(height: 10),
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
          itemExtent: 72,
          itemBuilder: (context, index) {
            final event = events[index];
            final isfirst = index == 0;
            final isLast = index == events.length - 1;

            return TimelineTile(
              alignment: TimelineAlign.start,
              isFirst: isfirst,
              isLast: isLast,
              beforeLineStyle: LineStyle(
                thickness: 8,
              ),
              indicatorStyle: IndicatorStyle(
                width: 40,
                indicatorXY: 0.0,
              ),
              endChild: Padding(
                padding: const EdgeInsets.only(left: 10),
                child: Column(
                  children: [
                    Row(
                      children: [
                        Text(
                          event.description!,
                          style: TextStyle(
                            color: Colors.black,
                            fontSize: 20,
                          ),
                        ),
                        Spacer(),
                        Text(dateFormat.format(event.date!)),
                      ],
                    ),
                    SizedBox(height: 10),
                    Row(
                      children: [
                        Text(event.vehicleOdometer.toString()),
                      ],
                    ),
                    !isLast ? Divider() : SizedBox()
                  ],
                ),
              ),
            );
          }),
    );
  }
}
