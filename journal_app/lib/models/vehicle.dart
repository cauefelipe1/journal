import 'package:journal_mobile_app/models/base/model_base.dart';
import 'package:json_annotation/json_annotation.dart';

part 'generated/vehicle.g.dart';

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
  int? typeId;
  int? brandId;
  int? mianDriverId;

  VehicleModel({
    this.id,
    this.secondaryId,
    this.modelName,
    this.nickname,
    this.modelYear,
    this.typeId,
    this.brandId,
    this.mianDriverId,
  });

  @override
  Map<String, dynamic> toJson() => _$VehicleModelToJson(this);

  factory VehicleModel.fromJson(Map<String, dynamic> json) => _$VehicleModelFromJson(json);
}
