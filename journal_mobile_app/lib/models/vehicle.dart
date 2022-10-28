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

  factory VehicleBrandModel.fromJson(Map<String, dynamic> json) =>
      _$VehicleBrandModelFromJson(json);
}
