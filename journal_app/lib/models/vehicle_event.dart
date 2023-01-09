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
  int? id;
  int? ownerDriverId;
  int? vehicleId;
  int? driverId;
  DateTime? date;
  int? vehicleOdometer;
  VehicleEventType? type;
  String? description;
  String? note;

  VehicleEventModel({
    this.id,
    this.ownerDriverId,
    this.vehicleId,
    this.driverId,
    this.date,
    this.vehicleOdometer,
    this.type,
    this.description,
    this.note,
  });

  @override
  Map<String, dynamic> toJson() => _$VehicleEventModelToJson(this);

  factory VehicleEventModel.fromJson(Map<String, dynamic> json) => _$VehicleEventModelFromJson(json);
}
