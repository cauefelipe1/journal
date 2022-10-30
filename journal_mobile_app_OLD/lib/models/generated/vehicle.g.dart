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
