import 'package:journal_mobile_app/models/base/model_base.dart';
import 'package:journal_mobile_app/models/user.dart';
import 'package:json_annotation/json_annotation.dart';

part 'generated/driver.g.dart';

@JsonSerializable()
class DriverModel extends Serializable {
  String id;
  String firstName;
  String lastName;
  String fullName;
  int countryId;

  DriverModel({
    required this.id,
    required this.firstName,
    required this.lastName,
    required this.fullName,
    required this.countryId,
  });

  @override
  Map<String, dynamic> toJson() => _$DriverModelToJson(this);

  factory DriverModel.fromJson(Map<String, dynamic> json) => _$DriverModelFromJson(json);
}

@JsonSerializable()
class LoggedDriverDataModel extends Serializable {
  final DriverModel driver;
  final UserData userData;

  LoggedDriverDataModel({required this.driver, required this.userData});

  @override
  Map<String, dynamic> toJson() => _$LoggedDriverDataModelToJson(this);

  factory LoggedDriverDataModel.fromJson(Map<String, dynamic> json) => _$LoggedDriverDataModelFromJson(json);
}
