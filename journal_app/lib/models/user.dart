import 'package:journal_mobile_app/models/base/model_base.dart';
import 'package:json_annotation/json_annotation.dart';

part 'generated/user.g.dart';

enum UserType {
  @JsonValue(1)
  standard,

  @JsonValue(2)
  premium
}

@JsonSerializable()
class UserData extends Serializable {
  final String id;
  UserType userType;
  String email;
  String username;

  UserData({required this.id, required this.userType, required this.email, required this.username});

  @override
  Map<String, dynamic> toJson() => _$UserDataToJson(this);

  factory UserData.fromJson(Map<String, dynamic> json) => _$UserDataFromJson(json);
}
