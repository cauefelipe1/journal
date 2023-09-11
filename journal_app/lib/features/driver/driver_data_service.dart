import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:journal_mobile_app/infrastructure/http/api_constants.dart';
import 'package:journal_mobile_app/infrastructure/http/authenticated_http_client.dart';
import 'package:journal_mobile_app/models/driver.dart';

final driverDataServiceProvider = Provider<IDriverDataService>((ref) {
  return DriverDataService(httpClient: ref.watch(httpClientProvider));
});

abstract class IDriverDataService {
  Future<LoggedDriverDataModel?> getLoggedDriver();
}

class DriverDataService implements IDriverDataService {
  late IAuthenticatedHttpClient _httpClient;

  DriverDataService({required IAuthenticatedHttpClient httpClient}) {
    _httpClient = httpClient;
  }

  @override
  Future<LoggedDriverDataModel?> getLoggedDriver() async {
    var requestResult = await _httpClient.executeAuthGet(ApiConstants.driver.logged);

    if (requestResult == null) {
      return null;
    }

    var loggedDriver = LoggedDriverDataModel.fromJson(requestResult["data"]);

    return loggedDriver;
  }
}
