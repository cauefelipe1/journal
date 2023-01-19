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
  final int? vehicleId;
  final DateFormat dateFormat = DateFormat.yMd();

  VehicleHistoryComponent({
    super.key,
    this.vehicleId,
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
                if (vehicleId == null) {
                  return _getShimmer();
                }

                return ref.watch(vehicleEventsProvider(vehicleId!)).when(
                      data: (events) => _getHistoryBody(events, context),
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

  // Currently, this is not in use, but it will be kept here for a while
  // Widget _getNoVehicleSelectedBody(BuildContext context) {
  //   return Container(
  //     width: double.infinity,
  //     child: Column(
  //       children: [
  //         SizedBox(
  //           height: 25,
  //         ),
  //         Icon(
  //           Icons.car_crash,
  //           size: 200,
  //           color: Colors.blueGrey,
  //         ),
  //         Text(
  //           context.l10n.noVehicleSelected,
  //           style: TextStyle(
  //             fontSize: 20,
  //           ),
  //         ),
  //       ],
  //     ),
  //   );
  // }

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

  Widget _getHistoryBody(List<VehicleEventModel> events, BuildContext context) {
    if (events.length <= 0) {
      return _getNoHistoryList(events, context);
    }

    return _getHistoryList(events);
  }

  Widget _getNoHistoryList(List<VehicleEventModel> events, BuildContext context) {
    return Container(
      width: double.infinity,
      child: Column(
        children: [
          SizedBox(
            height: 25,
          ),
          Icon(
            Icons.history,
            size: 200,
            color: Colors.blueGrey,
          ),
          Text(
            context.l10n.vehicleWithoutHistory,
            style: TextStyle(
              fontSize: 20,
            ),
          ),
        ],
      ),
    );
  }

  Widget _getHistoryList(List<VehicleEventModel> events) {
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
                thickness: 10,
                color: Colors.grey[800]!,
              ),
              indicatorStyle: IndicatorStyle(
                  width: 50,
                  indicatorXY: 0.0,
                  color: _getIndicatorColor(event.type),
                  iconStyle: IconStyle(
                    iconData: _getIndicatorIcon(event.type),
                    color: Colors.white,
                    fontSize: 35,
                  )),
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

  IconData _getIndicatorIcon(VehicleEventType? type) {
    if (type == null) {
      return Icons.question_mark;
    }

    switch (type) {
      case VehicleEventType.refueling:
        return Icons.local_gas_station;
      case VehicleEventType.expense:
        return Icons.credit_card_outlined;
      case VehicleEventType.income:
        return Icons.monetization_on_outlined;
      case VehicleEventType.maintenance:
        return Icons.precision_manufacturing;
      case VehicleEventType.route:
        return Icons.location_pin;
      default:
        return Icons.question_mark;
    }
  }

  Color _getIndicatorColor(VehicleEventType? type) {
    if (type == null) {
      return Colors.grey;
    }

    switch (type) {
      case VehicleEventType.refueling:
        return Colors.yellow[900]!;
      case VehicleEventType.expense:
        return Colors.red[900]!;
      case VehicleEventType.income:
        return Colors.green[700]!;
      case VehicleEventType.maintenance:
        return Colors.blueGrey[600]!;
      case VehicleEventType.route:
        return Colors.blue[800]!;
      default:
        return Colors.red[900]!;
    }
  }
}
