// GENERATED CODE - DO NOT MODIFY BY HAND

part of '../user.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserData _$UserDataFromJson(Map<String, dynamic> json) => UserData(
      userId: json['userId'] as int,
      userType: $enumDecode(_$UserTypeEnumMap, json['userType']),
      email: json['email'] as String,
      username: json['username'] as String,
      name: json['name'] as String,
      nickname: json['nickname'] as String,
      displayName: json['displayName'] as String,
    );

Map<String, dynamic> _$UserDataToJson(UserData instance) => <String, dynamic>{
      'userId': instance.userId,
      'userType': _$UserTypeEnumMap[instance.userType]!,
      'email': instance.email,
      'username': instance.username,
      'name': instance.name,
      'nickname': instance.nickname,
      'displayName': instance.displayName,
    };

const _$UserTypeEnumMap = {
  UserType.standard: 1,
  UserType.premium: 2,
};
