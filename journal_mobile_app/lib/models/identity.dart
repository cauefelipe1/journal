import 'package:journal_mobile_app/models/base/model_base.dart';
import 'package:json_annotation/json_annotation.dart';

part 'generated/identity.g.dart';

@JsonSerializable()
class UserLoginInput extends Serializable {
  final String email;

  final String password;

  UserLoginInput({required this.email, required this.password});

  factory UserLoginInput.fromJson(Map<String, dynamic> json) =>
      _$UserLoginInputFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$UserLoginInputToJson(this);
}

@JsonSerializable()
class UserLoginResult {
  String? token;
  String? refreshToken;
  List<String>? errors;

  UserLoginResult({this.token, this.refreshToken, this.errors});

  factory UserLoginResult.fromJson(Map<String, dynamic> json) =>
      _$UserLoginResultFromJson(json);

  Map<String, dynamic> toJson() => _$UserLoginResultToJson(this);
}

@JsonSerializable()
class RefreshTokenInput {
  String? token;
  String? refreshToken;

  RefreshTokenInput({this.token, this.refreshToken});

  factory RefreshTokenInput.fromJson(Map<String, dynamic> json) =>
      _$RefreshTokenInputFromJson(json);

  Map<String, dynamic> toJson() => _$RefreshTokenInputToJson(this);
}
