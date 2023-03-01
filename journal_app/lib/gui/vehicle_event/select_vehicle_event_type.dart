import 'package:flutter/material.dart';
import 'package:flutter_staggered_grid_view/flutter_staggered_grid_view.dart';
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
                "Choose the event type", // TODO: Translate it
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
                    child: _getCard("Refueling", VehicleEventType.refueling), // TODO: Translate it
                  ),
                  StaggeredGridTile.count(
                    crossAxisCellCount: 1,
                    mainAxisCellCount: 1,
                    child: _getCard("Maintenance", VehicleEventType.maintenance), // TODO: Translate it
                  ),
                  StaggeredGridTile.count(
                    crossAxisCellCount: 1,
                    mainAxisCellCount: 1,
                    child: _getCard("Expense", VehicleEventType.expense), // TODO: Translate it
                  ),
                  StaggeredGridTile.count(
                    crossAxisCellCount: 1,
                    mainAxisCellCount: 1,
                    child: _getCard("Income", VehicleEventType.income), // TODO: Translate it
                  ),
                  StaggeredGridTile.count(
                    crossAxisCellCount: 2,
                    mainAxisCellCount: 1,
                    child: _getCard("Route", VehicleEventType.route), // TODO: Translate it
                  ),
                ],
              ),
            ],
          ),
        ),
      ),
    );
  }

  Widget _getCard(String name, VehicleEventType type) {
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
                  name,
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
