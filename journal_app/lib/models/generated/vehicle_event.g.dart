// GENERATED CODE - DO NOT MODIFY BY HAND

part of '../vehicle_event.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

VehicleEventModel _$VehicleEventModelFromJson(Map<String, dynamic> json) =>
    VehicleEventModel(
      id: json['id'] as String,
      ownerDriverId: json['ownerDriverId'] as String,
      vehicleId: json['vehicleId'] as String,
      driverId: json['driverId'] as String,
      date: DateTime.parse(json['date'] as String),
      vehicleOdometer: json['vehicleOdometer'] as int,
      type: $enumDecode(_$VehicleEventTypeEnumMap, json['type']),
      description: json['description'] as String?,
      note: json['note'] as String?,
    );

Map<String, dynamic> _$VehicleEventModelToJson(VehicleEventModel instance) =>
    <String, dynamic>{
      'id': instance.id,
      'ownerDriverId': instance.ownerDriverId,
      'vehicleId': instance.vehicleId,
      'driverId': instance.driverId,
      'date': instance.date.toIso8601String(),
      'vehicleOdometer': instance.vehicleOdometer,
      'type': _$VehicleEventTypeEnumMap[instance.type]!,
      'description': instance.description,
      'note': instance.note,
    };

const _$VehicleEventTypeEnumMap = {
  VehicleEventType.refueling: 1,
  VehicleEventType.maintenance: 2,
  VehicleEventType.route: 3,
  VehicleEventType.expense: 4,
  VehicleEventType.income: 5,
};
