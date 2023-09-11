// GENERATED CODE - DO NOT MODIFY BY HAND

part of '../vehicle.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

VehicleBrandModel _$VehicleBrandModelFromJson(Map<String, dynamic> json) =>
    VehicleBrandModel(
      id: json['id'] as int?,
      name: json['name'] as String?,
      countryId: json['countryId'] as int?,
    );

Map<String, dynamic> _$VehicleBrandModelToJson(VehicleBrandModel instance) =>
    <String, dynamic>{
      'id': instance.id,
      'name': instance.name,
      'countryId': instance.countryId,
    };

VehicleModel _$VehicleModelFromJson(Map<String, dynamic> json) => VehicleModel(
      id: json['id'] as String?,
      modelName: json['modelName'] as String,
      nickname: json['nickname'] as String,
      modelYear: json['modelYear'] as int?,
      type: $enumDecodeNullable(_$VehicleTypeEnumMap, json['type']),
      brandId: json['brandId'] as String?,
      mianDriverId: json['mianDriverId'] as String?,
      displayName: json['displayName'] as String?,
    );

Map<String, dynamic> _$VehicleModelToJson(VehicleModel instance) =>
    <String, dynamic>{
      'id': instance.id,
      'modelName': instance.modelName,
      'nickname': instance.nickname,
      'modelYear': instance.modelYear,
      'type': _$VehicleTypeEnumMap[instance.type],
      'brandId': instance.brandId,
      'mianDriverId': instance.mianDriverId,
      'displayName': instance.displayName,
    };

const _$VehicleTypeEnumMap = {
  VehicleType.car: 1,
  VehicleType.truck: 2,
  VehicleType.motorcycle: 3,
  VehicleType.boat: 4,
  VehicleType.airplane: 5,
  VehicleType.helicopter: 6,
};
