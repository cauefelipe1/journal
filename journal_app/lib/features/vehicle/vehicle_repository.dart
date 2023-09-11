import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:journal_mobile_app/infrastructure/http/api_constants.dart';
import 'package:journal_mobile_app/infrastructure/http/authenticated_http_client.dart';
import 'package:journal_mobile_app/models/vehicle.dart';

final vehicleRepositoryProvider = Provider<IVehicleRepository>((ref) {
  return VehicleRepository(httpClient: ref.watch(httpClientProvider));
});

abstract class IVehicleRepository {
  Future<List<VehicleBrandModel>> getAllBrands();
  Future<List<VehicleModel>> getDriverVehicles(String driverId);
}

class VehicleRepository implements IVehicleRepository {
  late IAuthenticatedHttpClient _httpClient;

  VehicleRepository({required IAuthenticatedHttpClient httpClient}) {
    _httpClient = httpClient;
  }

  @override
  Future<List<VehicleBrandModel>> getAllBrands() async {
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

  @override
  Future<List<VehicleModel>> getDriverVehicles(String driverId) async {
    String path = "${ApiConstants.vehicle.driverVehicles}/$driverId";

    var requestResult = await _httpClient.executeAuthGet(path);

    if (requestResult == null) {
      return [];
    }

    var vehicles = <VehicleModel>[];
    for (var p in requestResult["data"]) {
      vehicles.add(VehicleModel.fromJson(p));
    }

    return vehicles;
  }
}
