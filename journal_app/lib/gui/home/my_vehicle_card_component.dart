import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:journal_mobile_app/models/vehicle.dart';

class MyVehicleCardComponent extends ConsumerWidget {
  static const double _CARD_WIDGET = 160;
  final String? name;
  final VehicleType type;
  final double? width;

  const MyVehicleCardComponent({
    Key? key,
    required this.name,
    required this.type,
    this.width,
  }) : super(key: key);

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return _getBody(context, ref);
  }

  Widget _getBody(BuildContext context, WidgetRef ref) {
    return SizedBox(
      width: width ?? _CARD_WIDGET,
      child: Card(
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(15),
        ),
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 15),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Image(
                image: AssetImage(_getCardIconPath()),
              ),
              const SizedBox(
                height: 5,
              ),
              Text(
                name!,
                style: const TextStyle(
                  fontSize: 18,
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  String _getCardIconPath() {
    const String basePath = "assets/images/vehicle_icons";

    switch (type) {
      case VehicleType.car:
        return "$basePath/car_1.png";

      case VehicleType.truck:
        return "$basePath/truck_1.png";

      case VehicleType.motorcycle:
        return "$basePath/motorcycle_1.png";

      case VehicleType.boat:
      case VehicleType.airplane:
      case VehicleType.helicopter:
        return "$basePath/car_1.png";
    }
  }
}
