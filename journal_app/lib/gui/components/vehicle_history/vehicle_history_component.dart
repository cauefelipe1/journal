import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:intl/intl.dart';
import 'package:journal_mobile_app/features/vehicle_event/vehicle_event_repository.dart';
import 'package:journal_mobile_app/l10n/app_localization_context.dart';
import 'package:journal_mobile_app/models/vehicle_event.dart';
import 'package:shimmer/shimmer.dart';
import 'package:timeline_tile/timeline_tile.dart';

var vehicleEventsProvider = FutureProvider.autoDispose.family<List<VehicleEventModel>, int>((ref, vehicleId) {
  return ref.watch(vehicleEventsRepositoryProvider).getVehicleEvents(vehicleId);
});

class VehicleHistoryComponent extends StatelessWidget {
  final int vehicleId;
  final DateFormat dateFormat = DateFormat.yMd();

  VehicleHistoryComponent({
    super.key,
    required this.vehicleId,
  });

  @override
  Widget build(BuildContext context) {
    return Expanded(
      child: Container(
        width: double.infinity,
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
                if (vehicleId <= 0) {
                  return _getNoVehicleSelectedBody(context);
                }

                return ref.watch(vehicleEventsProvider(vehicleId)).when(
                      data: _getComponentBody,
                      loading: _getShimmer,
                      error: (error, stackTrace) => Text(error.toString()),
                    );
              },
            ),
          ],
        ),
      ),
    );
  }

  Widget _getNoVehicleSelectedBody(BuildContext context) {
    return Container(
      width: double.infinity,
      child: Column(
        children: [
          SizedBox(
            height: 25,
          ),
          Icon(
            Icons.car_crash,
            size: 200,
            color: Colors.blueGrey,
          ),
          Text(
            context.l10n.noVehicleSelected,
            style: TextStyle(
              fontSize: 20,
            ),
          ),
        ],
      ),
    );
  }

  Widget _getShimmer() {
    return Shimmer.fromColors(
      baseColor: Colors.grey[400]!,
      highlightColor: Colors.white,
      child: Column(
        mainAxisAlignment: MainAxisAlignment.start,
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          _getShimmerTile(true, false),
          _getShimmerTile(false, false),
          _getShimmerTile(false, true),
        ],
      ),
    );
  }

  TimelineTile _getShimmerTile(bool isFirst, bool isLast) {
    return TimelineTile(
      isFirst: isFirst,
      isLast: isLast,
      beforeLineStyle: LineStyle(
        thickness: 8,
      ),
      indicatorStyle: IndicatorStyle(
        width: 40,
        indicatorXY: 0.0,
      ),
      endChild: Container(
        padding: EdgeInsets.symmetric(horizontal: 10),
        height: 72,
        width: double.infinity,
        child: Column(
          children: [
            Container(
              width: double.infinity,
              height: 20,
              color: Colors.white,
            ),
            SizedBox(height: 10),
            Container(
              width: double.infinity,
              height: 20,
              color: Colors.white,
            ),
            !isLast ? Divider() : SizedBox()
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
            final isFirst = index == 0;
            final isLast = index == events.length - 1;

            return TimelineTile(
              alignment: TimelineAlign.start,
              isFirst: isFirst,
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
