import 'package:get_it/get_it.dart';
import 'package:journal_mobile_app/features/identity/identity_data_service.dart';

final GetIt locator = GetIt.instance;

void setupLocator() {
  locator.registerFactory<IIdentityDataService>(() => IdentityDataService());
}
