import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:journal_mobile_app/infrastructure/http/api_constants.dart';
import 'package:journal_mobile_app/infrastructure/http/authenticated_http_client.dart';
import 'package:journal_mobile_app/models/vehicle_event.dart';

final vehicleEventsRepositoryProvider = Provider<IVehicleEventRepository>((ref) {
  return VehicleEventRepository(httpClient: ref.watch(httpClientProvider));
});

abstract class IVehicleEventRepository {
  Future<List<VehicleEventModel>> getVehicleEvents(String vehicleId);
}

class VehicleEventRepository implements IVehicleEventRepository {
  late IAuthenticatedHttpClient _httpClient;

  VehicleEventRepository({required IAuthenticatedHttpClient httpClient}) {
    _httpClient = httpClient;
  }

  @override
  Future<List<VehicleEventModel>> getVehicleEvents(String vehicleId) async {
    String path = "${ApiConstants.vehicleEvents.vehicleEventsByVehicle}/$vehicleId";

    var requestResult = await _httpClient.executeAuthGet(path);

    if (requestResult == null) {
      return [];
    }

    var events = <VehicleEventModel>[];
    for (var p in requestResult) {
      events.add(VehicleEventModel.fromJson(p));
    }

    return events;
  }
}
