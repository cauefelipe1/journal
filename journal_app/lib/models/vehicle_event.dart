import 'package:journal_mobile_app/models/base/model_base.dart';
import 'package:json_annotation/json_annotation.dart';

part 'generated/vehicle_event.g.dart';

enum VehicleEventType {
  @JsonValue(1)
  refueling,
  @JsonValue(2)
  maintenance,
  @JsonValue(3)
  route,
  @JsonValue(4)
  expense,
  @JsonValue(5)
  income
}

@JsonSerializable()
class VehicleEventModel extends Serializable {
  String id;
  String ownerDriverId;
  String vehicleId;
  String driverId;
  DateTime date;
  int vehicleOdometer;
  VehicleEventType type;
  String? description;
  String? note;

  VehicleEventModel({
    required this.id,
    required this.ownerDriverId,
    required this.vehicleId,
    required this.driverId,
    required this.date,
    required this.vehicleOdometer,
    required this.type,
    this.description,
    this.note,
  });

  @override
  Map<String, dynamic> toJson() => _$VehicleEventModelToJson(this);

  factory VehicleEventModel.fromJson(Map<String, dynamic> json) => _$VehicleEventModelFromJson(json);
}
