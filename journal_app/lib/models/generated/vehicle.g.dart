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
      id: json['id'] as int?,
      secondaryId: json['secondaryId'] as String?,
      modelName: json['modelName'] as String?,
      nickname: json['nickname'] as String?,
      modelYear: json['modelYear'] as int?,
      typeId: json['typeId'] as int?,
      brandId: json['brandId'] as int?,
      mianDriverId: json['mianDriverId'] as int?,
    );

Map<String, dynamic> _$VehicleModelToJson(VehicleModel instance) =>
    <String, dynamic>{
      'id': instance.id,
      'secondaryId': instance.secondaryId,
      'modelName': instance.modelName,
      'nickname': instance.nickname,
      'modelYear': instance.modelYear,
      'typeId': instance.typeId,
      'brandId': instance.brandId,
      'mianDriverId': instance.mianDriverId,
    };
