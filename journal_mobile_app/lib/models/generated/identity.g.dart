// GENERATED CODE - DO NOT MODIFY BY HAND

part of '../identity.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserLoginInput _$UserLoginInputFromJson(Map<String, dynamic> json) =>
    UserLoginInput(
      email: json['email'] as String,
      password: json['password'] as String,
    );

Map<String, dynamic> _$UserLoginInputToJson(UserLoginInput instance) =>
    <String, dynamic>{
      'email': instance.email,
      'password': instance.password,
    };

UserLoginResult _$UserLoginResultFromJson(Map<String, dynamic> json) =>
    UserLoginResult(
      token: json['token'] as String?,
      refreshToken: json['refreshToken'] as String?,
      errors:
          (json['errors'] as List<dynamic>?)?.map((e) => e as String).toList(),
    );

Map<String, dynamic> _$UserLoginResultToJson(UserLoginResult instance) =>
    <String, dynamic>{
      'token': instance.token,
      'refreshToken': instance.refreshToken,
      'errors': instance.errors,
    };
