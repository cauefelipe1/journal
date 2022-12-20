import 'dart:ui' as ui;

import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';

final appLocalizationsProvider = Provider<AppLocalizations>((ref) => lookupAppLocalizations(ui.window.locale));
