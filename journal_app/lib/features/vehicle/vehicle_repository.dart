import 'package:journal_mobile_app/infrastructure/http/api_constants.dart';
import 'package:journal_mobile_app/infrastructure/http/authenticated_http_client.dart';
import 'package:journal_mobile_app/models/vehicle.dart';

class VehicleRepository {
  late AuthenticatedHttpClient _httpClient;

  VehicleRepository() {
    _httpClient = AuthenticatedHttpClient();
  }

  Future<List<VehicleBrandModel>> getAllBrands() async {
    await Future.delayed(const Duration(seconds: 10));

    var requestResult = await _httpClient.executeAuthGet(ApiConstants.vehicle.allBrands);

    if (requestResult == null) {
      return [];
    }

    var brands = <VehicleBrandModel>[];
    for (var p in requestResult) {
      brands.add(VehicleBrandModel.fromJson(p));
    }

    return brands;
  }

  Future<List<VehicleModel>> getDriverVehicles(int driverId) async {
    String path = "${ApiConstants.vehicle.driverVehicles}/$driverId";

    var requestResult = await _httpClient.executeAuthGet(path);

    if (requestResult == null) {
      return [];
    }

    var brands = <VehicleModel>[];
    for (var p in requestResult) {
      brands.add(VehicleModel.fromJson(p));
    }

    return brands;
  }
}
