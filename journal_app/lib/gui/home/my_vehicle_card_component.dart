import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class MyVehicleCardComponent extends ConsumerWidget {
  static const double _CARD_WIDGET = 160;
  final String? name;
  final int type; //TODO: Change by an enum
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
          padding: const EdgeInsets.only(left: 10),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              const Icon(
                Icons.car_repair,
                size: 100,
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
}
