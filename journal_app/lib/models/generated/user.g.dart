// GENERATED CODE - DO NOT MODIFY BY HAND

part of '../user.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserData _$UserDataFromJson(Map<String, dynamic> json) => UserData(
      id: json['id'] as String,
      userType: $enumDecode(_$UserTypeEnumMap, json['userType']),
      email: json['email'] as String,
      username: json['username'] as String,
    );

Map<String, dynamic> _$UserDataToJson(UserData instance) => <String, dynamic>{
      'id': instance.id,
      'userType': _$UserTypeEnumMap[instance.userType]!,
      'email': instance.email,
      'username': instance.username,
    };

const _$UserTypeEnumMap = {
  UserType.standard: 1,
  UserType.premium: 2,
};
