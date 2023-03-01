import 'package:flutter/material.dart';
import 'package:flutter_staggered_grid_view/flutter_staggered_grid_view.dart';
import 'package:journal_mobile_app/l10n/app_localization_context.dart';
import 'package:journal_mobile_app/models/vehicle_event.dart';

class SelectVehicleEventType extends StatelessWidget {
  const SelectVehicleEventType({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.grey[200],
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.all(15.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                context.l10n.chooseEventTypeTitle,
                style: const TextStyle(
                  fontSize: 35,
                ),
              ),
              SizedBox(height: 15),
              StaggeredGrid.count(
                crossAxisCount: 2,
                mainAxisSpacing: 10,
                crossAxisSpacing: 10,
                children: [
                  StaggeredGridTile.count(
                    crossAxisCellCount: 1,
                    mainAxisCellCount: 1,
                    child: _getCard(context, VehicleEventType.refueling),
                  ),
                  StaggeredGridTile.count(
                    crossAxisCellCount: 1,
                    mainAxisCellCount: 1,
                    child: _getCard(context, VehicleEventType.maintenance),
                  ),
                  StaggeredGridTile.count(
                    crossAxisCellCount: 1,
                    mainAxisCellCount: 1,
                    child: _getCard(context, VehicleEventType.expense),
                  ),
                  StaggeredGridTile.count(
                    crossAxisCellCount: 1,
                    mainAxisCellCount: 1,
                    child: _getCard(context, VehicleEventType.income),
                  ),
                  StaggeredGridTile.count(
                    crossAxisCellCount: 2,
                    mainAxisCellCount: 1,
                    child: _getCard(context, VehicleEventType.route),
                  ),
                ],
              ),
            ],
          ),
        ),
      ),
    );
  }

  Widget _getCard(BuildContext context, VehicleEventType type) {
    return SizedBox(
      child: Card(
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(15),
        ),
        child: InkWell(
          onTap: () {
            //onPressed?.call(vehicleId);
          },
          child: Padding(
            padding: const EdgeInsets.symmetric(horizontal: 15),
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Container(
                  padding: EdgeInsets.all(10),
                  decoration: BoxDecoration(
                    color: _getIndicatorColor(type),
                    borderRadius: BorderRadius.circular(50),
                  ),
                  child: Icon(
                    _getIndicatorIcon(type),
                    size: 65,
                    color: Colors.white,
                  ),
                ),
                const SizedBox(
                  height: 10,
                ),
                Text(
                  _getCardLabel(context, type),
                  style: const TextStyle(
                    fontSize: 20,
                    fontWeight: FontWeight.w500,
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }

  String _getCardLabel(BuildContext context, VehicleEventType? type) {
    if (type == null) {
      return context.l10n.unknownText;
    }

    switch (type) {
      case VehicleEventType.refueling:
        return context.l10n.refuelingText;
      case VehicleEventType.expense:
        return context.l10n.expenseText;
      case VehicleEventType.income:
        return context.l10n.incomeText;
      case VehicleEventType.maintenance:
        return context.l10n.maintenanceText;
      case VehicleEventType.route:
        return context.l10n.routeText;
      default:
        return context.l10n.unknownText;
    }
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
