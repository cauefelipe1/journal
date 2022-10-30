import 'package:journal_mobile_app/infrastructure/http/api_constants.dart';
import 'package:journal_mobile_app/infrastructure/http/authenticated_http_client.dart';
import 'package:journal_mobile_app/models/vehicle.dart';

class VehicleDataService extends AuthenticatedHttpClient {
  Future<List<VehicleBrandModel>> getAllBrands() async {
    var requestResult = await executeAuthGet(ApiConstants.vehicle.allBrands);

    if (requestResult == null) {
      return [];
    }

    var result =
        requestResult.map((brand) => VehicleBrandModel.fromJson(brand));

    return result;
  }
}
