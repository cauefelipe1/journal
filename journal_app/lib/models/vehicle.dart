import 'package:journal_mobile_app/models/base/model_base.dart';
import 'package:json_annotation/json_annotation.dart';

part 'generated/vehicle.g.dart';

enum VehicleType {
  @JsonValue(1)
  car,
  @JsonValue(2)
  truck,
  @JsonValue(3)
  motorcycle,
  @JsonValue(4)
  boat,
  @JsonValue(5)
  airplane,
  @JsonValue(6)
  helicopter,
}

@JsonSerializable()
class VehicleBrandModel extends Serializable {
  int? id;
  String? name;
  int? countryId;

  VehicleBrandModel({this.id, this.name, this.countryId});

  @override
  Map<String, dynamic> toJson() => _$VehicleBrandModelToJson(this);

  factory VehicleBrandModel.fromJson(Map<String, dynamic> json) => _$VehicleBrandModelFromJson(json);
}

@JsonSerializable()
class VehicleModel extends Serializable {
  int? id;
  String? secondaryId;
  String? modelName;
  String? nickname;
  int? modelYear;
  VehicleType? type;
  int? brandId;
  int? mianDriverId;
  String? displayName;

  VehicleModel({
    this.id,
    this.secondaryId,
    this.modelName,
    this.nickname,
    this.modelYear,
    this.type,
    this.brandId,
    this.mianDriverId,
    this.displayName,
  });

  @override
  Map<String, dynamic> toJson() => _$VehicleModelToJson(this);

  factory VehicleModel.fromJson(Map<String, dynamic> json) => _$VehicleModelFromJson(json);
}
