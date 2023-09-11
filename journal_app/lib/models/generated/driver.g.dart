// GENERATED CODE - DO NOT MODIFY BY HAND

part of '../driver.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

DriverModel _$DriverModelFromJson(Map<String, dynamic> json) => DriverModel(
      id: json['id'] as String,
      firstName: json['firstName'] as String,
      lastName: json['lastName'] as String,
      fullName: json['fullName'] as String,
      countryId: json['countryId'] as int,
    );

Map<String, dynamic> _$DriverModelToJson(DriverModel instance) =>
    <String, dynamic>{
      'id': instance.id,
      'firstName': instance.firstName,
      'lastName': instance.lastName,
      'fullName': instance.fullName,
      'countryId': instance.countryId,
    };

LoggedDriverDataModel _$LoggedDriverDataModelFromJson(
        Map<String, dynamic> json) =>
    LoggedDriverDataModel(
      driver: DriverModel.fromJson(json['driver'] as Map<String, dynamic>),
      userData: UserData.fromJson(json['userData'] as Map<String, dynamic>),
    );

Map<String, dynamic> _$LoggedDriverDataModelToJson(
        LoggedDriverDataModel instance) =>
    <String, dynamic>{
      'driver': instance.driver,
      'userData': instance.userData,
    };
