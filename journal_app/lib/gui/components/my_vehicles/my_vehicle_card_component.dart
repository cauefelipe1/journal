import 'package:flutter/material.dart';
import 'package:journal_mobile_app/models/vehicle.dart';

class MyVehicleCardComponent extends StatelessWidget {
  static const double _CARD_WIDGET = 160;

  final int vehicleId;
  final VehicleType type;
  final String? name;
  final double? width;

  final ValueChanged<int>? onPressed;

  const MyVehicleCardComponent({
    Key? key,
    required this.vehicleId,
    required this.type,
    required this.name,
    this.width,
    this.onPressed,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return _getBody(context);
  }

  Widget _getBody(BuildContext context) {
    return SizedBox(
      width: width ?? _CARD_WIDGET,
      child: Card(
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(15),
        ),
        child: InkWell(
          onTap: () {
            onPressed?.call(vehicleId);
          },
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
