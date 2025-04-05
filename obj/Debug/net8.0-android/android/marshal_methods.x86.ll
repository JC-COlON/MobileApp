; ModuleID = 'marshal_methods.x86.ll'
source_filename = "marshal_methods.x86.ll"
target datalayout = "e-m:e-p:32:32-p270:32:32-p271:32:32-p272:64:64-f64:32:64-f80:32-n8:16:32-S128"
target triple = "i686-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [328 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [650 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 67
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 66
	i32 15721112, ; 2: System.Runtime.Intrinsics.dll => 0xefe298 => 107
	i32 32687329, ; 3: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 245
	i32 34715100, ; 4: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 279
	i32 34839235, ; 5: System.IO.FileSystem.DriveInfo => 0x2139ac3 => 47
	i32 39109920, ; 6: Newtonsoft.Json.dll => 0x254c520 => 201
	i32 39485524, ; 7: System.Net.WebSockets.dll => 0x25a8054 => 79
	i32 42639949, ; 8: System.Threading.Thread => 0x28aa24d => 144
	i32 66541672, ; 9: System.Diagnostics.StackTrace => 0x3f75868 => 29
	i32 67008169, ; 10: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 320
	i32 68219467, ; 11: System.Security.Cryptography.Primitives => 0x410f24b => 123
	i32 72070932, ; 12: Microsoft.Maui.Graphics.dll => 0x44bb714 => 200
	i32 82292897, ; 13: System.Runtime.CompilerServices.VisualC.dll => 0x4e7b0a1 => 101
	i32 98325684, ; 14: Microsoft.Extensions.Diagnostics.Abstractions => 0x5dc54b4 => 183
	i32 101534019, ; 15: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 263
	i32 117431740, ; 16: System.Runtime.InteropServices => 0x6ffddbc => 106
	i32 120558881, ; 17: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 263
	i32 122350210, ; 18: System.Threading.Channels.dll => 0x74aea82 => 138
	i32 134690465, ; 19: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 283
	i32 142721839, ; 20: System.Net.WebHeaderCollection => 0x881c32f => 76
	i32 149972175, ; 21: System.Security.Cryptography.Primitives.dll => 0x8f064cf => 123
	i32 159306688, ; 22: System.ComponentModel.Annotations => 0x97ed3c0 => 13
	i32 165246403, ; 23: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 219
	i32 176265551, ; 24: System.ServiceProcess => 0xa81994f => 131
	i32 182336117, ; 25: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 265
	i32 184328833, ; 26: System.ValueTuple.dll => 0xafca281 => 150
	i32 195452805, ; 27: vi/Microsoft.Maui.Controls.resources.dll => 0xba65f85 => 317
	i32 199333315, ; 28: zh-HK/Microsoft.Maui.Controls.resources.dll => 0xbe195c3 => 318
	i32 205061960, ; 29: System.ComponentModel => 0xc38ff48 => 18
	i32 209399409, ; 30: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 217
	i32 220171995, ; 31: System.Diagnostics.Debug => 0xd1f8edb => 26
	i32 221958352, ; 32: Microsoft.Extensions.Diagnostics.dll => 0xd3ad0d0 => 182
	i32 230216969, ; 33: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 239
	i32 230752869, ; 34: Microsoft.CSharp.dll => 0xdc10265 => 1
	i32 231409092, ; 35: System.Linq.Parallel => 0xdcb05c4 => 58
	i32 231814094, ; 36: System.Globalization => 0xdd133ce => 41
	i32 246610117, ; 37: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 90
	i32 261689757, ; 38: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 222
	i32 276479776, ; 39: System.Threading.Timer.dll => 0x107abf20 => 146
	i32 278686392, ; 40: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 241
	i32 280482487, ; 41: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 238
	i32 280992041, ; 42: cs/Microsoft.Maui.Controls.resources.dll => 0x10bf9929 => 289
	i32 291076382, ; 43: System.IO.Pipes.AccessControl.dll => 0x1159791e => 53
	i32 291275502, ; 44: Microsoft.Extensions.Http.dll => 0x115c82ee => 184
	i32 298918909, ; 45: System.Net.Ping.dll => 0x11d123fd => 68
	i32 317674968, ; 46: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 317
	i32 318968648, ; 47: Xamarin.AndroidX.Activity.dll => 0x13031348 => 208
	i32 321597661, ; 48: System.Numerics => 0x132b30dd => 82
	i32 336156722, ; 49: ja/Microsoft.Maui.Controls.resources.dll => 0x14095832 => 302
	i32 342366114, ; 50: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 240
	i32 356389973, ; 51: it/Microsoft.Maui.Controls.resources.dll => 0x153e1455 => 301
	i32 360082299, ; 52: System.ServiceModel.Web => 0x15766b7b => 130
	i32 367780167, ; 53: System.IO.Pipes => 0x15ebe147 => 54
	i32 374914964, ; 54: System.Transactions.Local => 0x1658bf94 => 148
	i32 375677976, ; 55: System.Net.ServicePoint.dll => 0x16646418 => 73
	i32 379916513, ; 56: System.Threading.Thread.dll => 0x16a510e1 => 144
	i32 382771021, ; 57: Microsoft.Bcl.Memory.dll => 0x16d09f4d => 176
	i32 385762202, ; 58: System.Memory.dll => 0x16fe439a => 61
	i32 392610295, ; 59: System.Threading.ThreadPool.dll => 0x1766c1f7 => 145
	i32 395744057, ; 60: _Microsoft.Android.Resource.Designer => 0x17969339 => 324
	i32 403441872, ; 61: WindowsBase => 0x180c08d0 => 164
	i32 435591531, ; 62: sv/Microsoft.Maui.Controls.resources.dll => 0x19f6996b => 313
	i32 441335492, ; 63: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 223
	i32 442565967, ; 64: System.Collections => 0x1a61054f => 12
	i32 450948140, ; 65: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 236
	i32 451504562, ; 66: System.Security.Cryptography.X509Certificates => 0x1ae969b2 => 124
	i32 456227837, ; 67: System.Web.HttpUtility.dll => 0x1b317bfd => 151
	i32 459347974, ; 68: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 112
	i32 465846621, ; 69: mscorlib => 0x1bc4415d => 165
	i32 469710990, ; 70: System.dll => 0x1bff388e => 163
	i32 476646585, ; 71: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 238
	i32 485463106, ; 72: Microsoft.IdentityModel.Abstractions => 0x1cef9442 => 191
	i32 486930444, ; 73: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 251
	i32 498788369, ; 74: System.ObjectModel => 0x1dbae811 => 83
	i32 500358224, ; 75: id/Microsoft.Maui.Controls.resources.dll => 0x1dd2dc50 => 300
	i32 503918385, ; 76: fi/Microsoft.Maui.Controls.resources.dll => 0x1e092f31 => 294
	i32 513247710, ; 77: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 190
	i32 526420162, ; 78: System.Transactions.dll => 0x1f6088c2 => 149
	i32 527452488, ; 79: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 283
	i32 530272170, ; 80: System.Linq.Queryable => 0x1f9b4faa => 59
	i32 539058512, ; 81: Microsoft.Extensions.Logging => 0x20216150 => 185
	i32 540030774, ; 82: System.IO.FileSystem.dll => 0x20303736 => 50
	i32 545304856, ; 83: System.Runtime.Extensions => 0x2080b118 => 102
	i32 546455878, ; 84: System.Runtime.Serialization.Xml => 0x20924146 => 113
	i32 549171840, ; 85: System.Globalization.Calendars => 0x20bbb280 => 39
	i32 557405415, ; 86: Jsr305Binding => 0x213954e7 => 276
	i32 569601784, ; 87: Xamarin.AndroidX.Window.Extensions.Core.Core => 0x21f36ef8 => 274
	i32 577335427, ; 88: System.Security.Cryptography.Cng => 0x22697083 => 119
	i32 592146354, ; 89: pt-BR/Microsoft.Maui.Controls.resources.dll => 0x234b6fb2 => 308
	i32 597488923, ; 90: CommunityToolkit.Maui => 0x239cf51b => 172
	i32 601371474, ; 91: System.IO.IsolatedStorage.dll => 0x23d83352 => 51
	i32 605376203, ; 92: System.IO.Compression.FileSystem => 0x24154ecb => 43
	i32 613668793, ; 93: System.Security.Cryptography.Algorithms => 0x2493d7b9 => 118
	i32 627609679, ; 94: Xamarin.AndroidX.CustomView => 0x2568904f => 228
	i32 627931235, ; 95: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 306
	i32 639843206, ; 96: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x26233b86 => 234
	i32 643868501, ; 97: System.Net => 0x2660a755 => 80
	i32 662205335, ; 98: System.Text.Encodings.Web.dll => 0x27787397 => 135
	i32 663517072, ; 99: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 270
	i32 666292255, ; 100: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 215
	i32 672442732, ; 101: System.Collections.Concurrent => 0x2814a96c => 8
	i32 683518922, ; 102: System.Net.Security => 0x28bdabca => 72
	i32 688181140, ; 103: ca/Microsoft.Maui.Controls.resources.dll => 0x2904cf94 => 288
	i32 690569205, ; 104: System.Xml.Linq.dll => 0x29293ff5 => 154
	i32 691348768, ; 105: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 285
	i32 693804605, ; 106: System.Windows => 0x295a9e3d => 153
	i32 699345723, ; 107: System.Reflection.Emit => 0x29af2b3b => 91
	i32 700284507, ; 108: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 280
	i32 700358131, ; 109: System.IO.Compression.ZipFile => 0x29be9df3 => 44
	i32 706645707, ; 110: ko/Microsoft.Maui.Controls.resources.dll => 0x2a1e8ecb => 303
	i32 709557578, ; 111: de/Microsoft.Maui.Controls.resources.dll => 0x2a4afd4a => 291
	i32 720511267, ; 112: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 284
	i32 722857257, ; 113: System.Runtime.Loader.dll => 0x2b15ed29 => 108
	i32 731701662, ; 114: Microsoft.Extensions.Options.ConfigurationExtensions => 0x2b9ce19e => 189
	i32 735137430, ; 115: System.Security.SecureString.dll => 0x2bd14e96 => 128
	i32 752232764, ; 116: System.Diagnostics.Contracts.dll => 0x2cd6293c => 25
	i32 755313932, ; 117: Xamarin.Android.Glide.Annotations.dll => 0x2d052d0c => 205
	i32 759454413, ; 118: System.Net.Requests => 0x2d445acd => 71
	i32 762598435, ; 119: System.IO.Pipes.dll => 0x2d745423 => 54
	i32 775507847, ; 120: System.IO.Compression => 0x2e394f87 => 45
	i32 777317022, ; 121: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 312
	i32 789151979, ; 122: Microsoft.Extensions.Options => 0x2f0980eb => 188
	i32 790371945, ; 123: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 0x2f1c1e69 => 229
	i32 804715423, ; 124: System.Data.Common => 0x2ff6fb9f => 22
	i32 807930345, ; 125: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 0x302809e9 => 243
	i32 823281589, ; 126: System.Private.Uri.dll => 0x311247b5 => 85
	i32 830298997, ; 127: System.IO.Compression.Brotli => 0x317d5b75 => 42
	i32 832635846, ; 128: System.Xml.XPath.dll => 0x31a103c6 => 159
	i32 834051424, ; 129: System.Net.Quic => 0x31b69d60 => 70
	i32 843511501, ; 130: Xamarin.AndroidX.Print => 0x3246f6cd => 256
	i32 873119928, ; 131: Microsoft.VisualBasic => 0x340ac0b8 => 3
	i32 877678880, ; 132: System.Globalization.dll => 0x34505120 => 41
	i32 878954865, ; 133: System.Net.Http.Json => 0x3463c971 => 62
	i32 904024072, ; 134: System.ComponentModel.Primitives.dll => 0x35e25008 => 16
	i32 911108515, ; 135: System.IO.MemoryMappedFiles.dll => 0x364e69a3 => 52
	i32 926902833, ; 136: tr/Microsoft.Maui.Controls.resources.dll => 0x373f6a31 => 315
	i32 928116545, ; 137: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 279
	i32 952186615, ; 138: System.Runtime.InteropServices.JavaScript.dll => 0x38c136f7 => 104
	i32 955402788, ; 139: Newtonsoft.Json => 0x38f24a24 => 201
	i32 956575887, ; 140: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 284
	i32 966729478, ; 141: Xamarin.Google.Crypto.Tink.Android => 0x399f1f06 => 277
	i32 967690846, ; 142: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 240
	i32 975236339, ; 143: System.Diagnostics.Tracing => 0x3a20ecf3 => 33
	i32 975874589, ; 144: System.Xml.XDocument => 0x3a2aaa1d => 157
	i32 986514023, ; 145: System.Private.DataContractSerialization.dll => 0x3acd0267 => 84
	i32 987214855, ; 146: System.Diagnostics.Tools => 0x3ad7b407 => 31
	i32 992768348, ; 147: System.Collections.dll => 0x3b2c715c => 12
	i32 994442037, ; 148: System.IO.FileSystem => 0x3b45fb35 => 50
	i32 1001831731, ; 149: System.IO.UnmanagedMemoryStream.dll => 0x3bb6bd33 => 55
	i32 1012816738, ; 150: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 260
	i32 1019214401, ; 151: System.Drawing => 0x3cbffa41 => 35
	i32 1028951442, ; 152: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 181
	i32 1029334545, ; 153: da/Microsoft.Maui.Controls.resources.dll => 0x3d5a6611 => 290
	i32 1031528504, ; 154: Xamarin.Google.ErrorProne.Annotations.dll => 0x3d7be038 => 278
	i32 1035644815, ; 155: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 213
	i32 1036536393, ; 156: System.Drawing.Primitives.dll => 0x3dc84a49 => 34
	i32 1044663988, ; 157: System.Linq.Expressions.dll => 0x3e444eb4 => 57
	i32 1048992957, ; 158: Microsoft.Extensions.Diagnostics.Abstractions.dll => 0x3e865cbd => 183
	i32 1052210849, ; 159: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 247
	i32 1067306892, ; 160: GoogleGson => 0x3f9dcf8c => 175
	i32 1082857460, ; 161: System.ComponentModel.TypeConverter => 0x408b17f4 => 17
	i32 1084122840, ; 162: Xamarin.Kotlin.StdLib => 0x409e66d8 => 281
	i32 1098259244, ; 163: System => 0x41761b2c => 163
	i32 1118262833, ; 164: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 303
	i32 1121599056, ; 165: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 0x42da3e50 => 246
	i32 1127624469, ; 166: Microsoft.Extensions.Logging.Debug => 0x43362f15 => 187
	i32 1149092582, ; 167: Xamarin.AndroidX.Window => 0x447dc2e6 => 273
	i32 1162065116, ; 168: Microsoft.Bcl.Memory => 0x4543b4dc => 176
	i32 1168523401, ; 169: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 309
	i32 1170634674, ; 170: System.Web.dll => 0x45c677b2 => 152
	i32 1175144683, ; 171: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 269
	i32 1178241025, ; 172: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 254
	i32 1203215381, ; 173: pl/Microsoft.Maui.Controls.resources.dll => 0x47b79c15 => 307
	i32 1204270330, ; 174: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 215
	i32 1208641965, ; 175: System.Diagnostics.Process => 0x480a69ad => 28
	i32 1214827643, ; 176: CommunityToolkit.Mvvm => 0x4868cc7b => 174
	i32 1219128291, ; 177: System.IO.IsolatedStorage => 0x48aa6be3 => 51
	i32 1234928153, ; 178: nb/Microsoft.Maui.Controls.resources.dll => 0x499b8219 => 305
	i32 1243150071, ; 179: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 0x4a18f6f7 => 274
	i32 1253011324, ; 180: Microsoft.Win32.Registry => 0x4aaf6f7c => 5
	i32 1260983243, ; 181: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 289
	i32 1264511973, ; 182: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 264
	i32 1267360935, ; 183: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 268
	i32 1273260888, ; 184: Xamarin.AndroidX.Collection.Ktx => 0x4be46b58 => 220
	i32 1275534314, ; 185: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 285
	i32 1278448581, ; 186: Xamarin.AndroidX.Annotation.Jvm => 0x4c3393c5 => 212
	i32 1293217323, ; 187: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 231
	i32 1309188875, ; 188: System.Private.DataContractSerialization => 0x4e08a30b => 84
	i32 1322716291, ; 189: Xamarin.AndroidX.Window.dll => 0x4ed70c83 => 273
	i32 1324164729, ; 190: System.Linq => 0x4eed2679 => 60
	i32 1335329327, ; 191: System.Runtime.Serialization.Json.dll => 0x4f97822f => 111
	i32 1364015309, ; 192: System.IO => 0x514d38cd => 56
	i32 1373134921, ; 193: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 319
	i32 1376866003, ; 194: Xamarin.AndroidX.SavedState => 0x52114ed3 => 260
	i32 1379779777, ; 195: System.Resources.ResourceManager => 0x523dc4c1 => 98
	i32 1402170036, ; 196: System.Configuration.dll => 0x53936ab4 => 19
	i32 1406073936, ; 197: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 224
	i32 1408764838, ; 198: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 110
	i32 1411638395, ; 199: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 100
	i32 1422545099, ; 200: System.Runtime.CompilerServices.VisualC => 0x54ca50cb => 101
	i32 1430672901, ; 201: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 287
	i32 1434145427, ; 202: System.Runtime.Handles => 0x557b5293 => 103
	i32 1435222561, ; 203: Xamarin.Google.Crypto.Tink.Android.dll => 0x558bc221 => 277
	i32 1439761251, ; 204: System.Net.Quic.dll => 0x55d10363 => 70
	i32 1452070440, ; 205: System.Formats.Asn1.dll => 0x568cd628 => 37
	i32 1453312822, ; 206: System.Diagnostics.Tools.dll => 0x569fcb36 => 31
	i32 1457743152, ; 207: System.Runtime.Extensions.dll => 0x56e36530 => 102
	i32 1458022317, ; 208: System.Net.Security.dll => 0x56e7a7ad => 72
	i32 1460893475, ; 209: System.IdentityModel.Tokens.Jwt => 0x57137723 => 203
	i32 1461004990, ; 210: es\Microsoft.Maui.Controls.resources => 0x57152abe => 293
	i32 1461234159, ; 211: System.Collections.Immutable.dll => 0x5718a9ef => 9
	i32 1461719063, ; 212: System.Security.Cryptography.OpenSsl => 0x57201017 => 122
	i32 1462112819, ; 213: System.IO.Compression.dll => 0x57261233 => 45
	i32 1469204771, ; 214: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 214
	i32 1470490898, ; 215: Microsoft.Extensions.Primitives => 0x57a5e912 => 190
	i32 1479771757, ; 216: System.Collections.Immutable => 0x5833866d => 9
	i32 1480492111, ; 217: System.IO.Compression.Brotli.dll => 0x583e844f => 42
	i32 1487239319, ; 218: Microsoft.Win32.Primitives => 0x58a57897 => 4
	i32 1490025113, ; 219: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 0x58cffa99 => 261
	i32 1493001747, ; 220: hi/Microsoft.Maui.Controls.resources.dll => 0x58fd6613 => 297
	i32 1498168481, ; 221: Microsoft.IdentityModel.JsonWebTokens.dll => 0x594c3ca1 => 192
	i32 1505131794, ; 222: Microsoft.Extensions.Http => 0x59b67d12 => 184
	i32 1514721132, ; 223: el/Microsoft.Maui.Controls.resources.dll => 0x5a48cf6c => 292
	i32 1536373174, ; 224: System.Diagnostics.TextWriterTraceListener => 0x5b9331b6 => 30
	i32 1543031311, ; 225: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 137
	i32 1543355203, ; 226: System.Reflection.Emit.dll => 0x5bfdbb43 => 91
	i32 1550322496, ; 227: System.Reflection.Extensions.dll => 0x5c680b40 => 92
	i32 1551623176, ; 228: sk/Microsoft.Maui.Controls.resources.dll => 0x5c7be408 => 312
	i32 1565862583, ; 229: System.IO.FileSystem.Primitives => 0x5d552ab7 => 48
	i32 1566207040, ; 230: System.Threading.Tasks.Dataflow.dll => 0x5d5a6c40 => 140
	i32 1573704789, ; 231: System.Runtime.Serialization.Json => 0x5dccd455 => 111
	i32 1580037396, ; 232: System.Threading.Overlapped => 0x5e2d7514 => 139
	i32 1582372066, ; 233: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 230
	i32 1592978981, ; 234: System.Runtime.Serialization.dll => 0x5ef2ee25 => 114
	i32 1597949149, ; 235: Xamarin.Google.ErrorProne.Annotations => 0x5f3ec4dd => 278
	i32 1601112923, ; 236: System.Xml.Serialization => 0x5f6f0b5b => 156
	i32 1603525486, ; 237: Microsoft.Maui.Controls.HotReload.Forms.dll => 0x5f93db6e => 321
	i32 1604827217, ; 238: System.Net.WebClient => 0x5fa7b851 => 75
	i32 1618516317, ; 239: System.Net.WebSockets.Client.dll => 0x6078995d => 78
	i32 1622152042, ; 240: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 250
	i32 1622358360, ; 241: System.Dynamic.Runtime => 0x60b33958 => 36
	i32 1624863272, ; 242: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 272
	i32 1634654947, ; 243: CommunityToolkit.Maui.Core.dll => 0x616edae3 => 173
	i32 1635184631, ; 244: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x6176eff7 => 234
	i32 1636350590, ; 245: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 227
	i32 1639515021, ; 246: System.Net.Http.dll => 0x61b9038d => 63
	i32 1639986890, ; 247: System.Text.RegularExpressions => 0x61c036ca => 137
	i32 1641389582, ; 248: System.ComponentModel.EventBasedAsync.dll => 0x61d59e0e => 15
	i32 1657153582, ; 249: System.Runtime => 0x62c6282e => 115
	i32 1658241508, ; 250: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 266
	i32 1658251792, ; 251: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 275
	i32 1670060433, ; 252: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 222
	i32 1675553242, ; 253: System.IO.FileSystem.DriveInfo.dll => 0x63dee9da => 47
	i32 1677501392, ; 254: System.Net.Primitives.dll => 0x63fca3d0 => 69
	i32 1678508291, ; 255: System.Net.WebSockets => 0x640c0103 => 79
	i32 1679769178, ; 256: System.Security.Cryptography => 0x641f3e5a => 125
	i32 1691477237, ; 257: System.Reflection.Metadata => 0x64d1e4f5 => 93
	i32 1696967625, ; 258: System.Security.Cryptography.Csp => 0x6525abc9 => 120
	i32 1698840827, ; 259: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 282
	i32 1701541528, ; 260: System.Diagnostics.Debug.dll => 0x656b7698 => 26
	i32 1720223769, ; 261: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 0x66888819 => 243
	i32 1726116996, ; 262: System.Reflection.dll => 0x66e27484 => 96
	i32 1728033016, ; 263: System.Diagnostics.FileVersionInfo.dll => 0x66ffb0f8 => 27
	i32 1729485958, ; 264: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 218
	i32 1736233607, ; 265: ro/Microsoft.Maui.Controls.resources.dll => 0x677cd287 => 310
	i32 1743415430, ; 266: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 288
	i32 1744735666, ; 267: System.Transactions.Local.dll => 0x67fe8db2 => 148
	i32 1746316138, ; 268: Mono.Android.Export => 0x6816ab6a => 168
	i32 1750313021, ; 269: Microsoft.Win32.Primitives.dll => 0x6853a83d => 4
	i32 1758240030, ; 270: System.Resources.Reader.dll => 0x68cc9d1e => 97
	i32 1763938596, ; 271: System.Diagnostics.TraceSource.dll => 0x69239124 => 32
	i32 1765942094, ; 272: System.Reflection.Extensions => 0x6942234e => 92
	i32 1766324549, ; 273: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 265
	i32 1770582343, ; 274: Microsoft.Extensions.Logging.dll => 0x6988f147 => 185
	i32 1776026572, ; 275: System.Core.dll => 0x69dc03cc => 21
	i32 1777075843, ; 276: System.Globalization.Extensions.dll => 0x69ec0683 => 40
	i32 1780572499, ; 277: Mono.Android.Runtime.dll => 0x6a216153 => 169
	i32 1782862114, ; 278: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 304
	i32 1788241197, ; 279: Xamarin.AndroidX.Fragment => 0x6a96652d => 236
	i32 1793755602, ; 280: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 296
	i32 1808609942, ; 281: Xamarin.AndroidX.Loader => 0x6bcd3296 => 250
	i32 1813058853, ; 282: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 281
	i32 1813201214, ; 283: Xamarin.Google.Android.Material => 0x6c13413e => 275
	i32 1818569960, ; 284: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 255
	i32 1818787751, ; 285: Microsoft.VisualBasic.Core => 0x6c687fa7 => 2
	i32 1824175904, ; 286: System.Text.Encoding.Extensions => 0x6cbab720 => 133
	i32 1824722060, ; 287: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 110
	i32 1827303595, ; 288: Microsoft.VisualStudio.DesignTools.TapContract => 0x6cea70ab => 323
	i32 1828327463, ; 289: DigesettAPP => 0x6cfa1027 => 0
	i32 1828688058, ; 290: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 186
	i32 1842015223, ; 291: uk/Microsoft.Maui.Controls.resources.dll => 0x6dcaebf7 => 316
	i32 1847515442, ; 292: Xamarin.Android.Glide.Annotations => 0x6e1ed932 => 205
	i32 1853025655, ; 293: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 313
	i32 1858542181, ; 294: System.Linq.Expressions => 0x6ec71a65 => 57
	i32 1870277092, ; 295: System.Reflection.Primitives => 0x6f7a29e4 => 94
	i32 1875935024, ; 296: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 295
	i32 1879696579, ; 297: System.Formats.Tar.dll => 0x7009e4c3 => 38
	i32 1885316902, ; 298: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 216
	i32 1885918049, ; 299: Microsoft.VisualStudio.DesignTools.TapContract.dll => 0x7068d361 => 323
	i32 1888955245, ; 300: System.Diagnostics.Contracts => 0x70972b6d => 25
	i32 1889954781, ; 301: System.Reflection.Metadata.dll => 0x70a66bdd => 93
	i32 1898237753, ; 302: System.Reflection.DispatchProxy => 0x7124cf39 => 88
	i32 1900610850, ; 303: System.Resources.ResourceManager.dll => 0x71490522 => 98
	i32 1910275211, ; 304: System.Collections.NonGeneric.dll => 0x71dc7c8b => 10
	i32 1939592360, ; 305: System.Private.Xml.Linq => 0x739bd4a8 => 86
	i32 1956758971, ; 306: System.Resources.Writer => 0x74a1c5bb => 99
	i32 1961813231, ; 307: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x74eee4ef => 262
	i32 1968388702, ; 308: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 177
	i32 1983156543, ; 309: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 282
	i32 1985761444, ; 310: Xamarin.Android.Glide.GifDecoder => 0x765c50a4 => 207
	i32 1986222447, ; 311: Microsoft.IdentityModel.Tokens.dll => 0x7663596f => 194
	i32 2003115576, ; 312: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 292
	i32 2011961780, ; 313: System.Buffers.dll => 0x77ec19b4 => 7
	i32 2019465201, ; 314: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 247
	i32 2025202353, ; 315: ar/Microsoft.Maui.Controls.resources.dll => 0x78b622b1 => 287
	i32 2031763787, ; 316: Xamarin.Android.Glide => 0x791a414b => 204
	i32 2045470958, ; 317: System.Private.Xml => 0x79eb68ee => 87
	i32 2048278909, ; 318: Microsoft.Extensions.Configuration.Binder.dll => 0x7a16417d => 179
	i32 2055257422, ; 319: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 242
	i32 2060060697, ; 320: System.Windows.dll => 0x7aca0819 => 153
	i32 2066184531, ; 321: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 291
	i32 2070888862, ; 322: System.Diagnostics.TraceSource => 0x7b6f419e => 32
	i32 2079903147, ; 323: System.Runtime.dll => 0x7bf8cdab => 115
	i32 2090596640, ; 324: System.Numerics.Vectors => 0x7c9bf920 => 81
	i32 2127167465, ; 325: System.Console => 0x7ec9ffe9 => 20
	i32 2142473426, ; 326: System.Collections.Specialized => 0x7fb38cd2 => 11
	i32 2143790110, ; 327: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 161
	i32 2146852085, ; 328: Microsoft.VisualBasic.dll => 0x7ff65cf5 => 3
	i32 2159891885, ; 329: Microsoft.Maui => 0x80bd55ad => 198
	i32 2169148018, ; 330: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 299
	i32 2181898931, ; 331: Microsoft.Extensions.Options.dll => 0x820d22b3 => 188
	i32 2192057212, ; 332: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 186
	i32 2193016926, ; 333: System.ObjectModel.dll => 0x82b6c85e => 83
	i32 2201107256, ; 334: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 286
	i32 2201231467, ; 335: System.Net.Http => 0x8334206b => 63
	i32 2207618523, ; 336: it\Microsoft.Maui.Controls.resources => 0x839595db => 301
	i32 2217644978, ; 337: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 269
	i32 2222056684, ; 338: System.Threading.Tasks.Parallel => 0x8471e4ec => 142
	i32 2244775296, ; 339: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 251
	i32 2252106437, ; 340: System.Xml.Serialization.dll => 0x863c6ac5 => 156
	i32 2256313426, ; 341: System.Globalization.Extensions => 0x867c9c52 => 40
	i32 2265110946, ; 342: System.Security.AccessControl.dll => 0x8702d9a2 => 116
	i32 2266799131, ; 343: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 178
	i32 2267999099, ; 344: Xamarin.Android.Glide.DiskLruCache.dll => 0x872eeb7b => 206
	i32 2270573516, ; 345: fr/Microsoft.Maui.Controls.resources.dll => 0x875633cc => 295
	i32 2279755925, ; 346: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 258
	i32 2293034957, ; 347: System.ServiceModel.Web.dll => 0x88acefcd => 130
	i32 2295906218, ; 348: System.Net.Sockets => 0x88d8bfaa => 74
	i32 2298471582, ; 349: System.Net.Mail => 0x88ffe49e => 65
	i32 2303942373, ; 350: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 305
	i32 2305521784, ; 351: System.Private.CoreLib.dll => 0x896b7878 => 171
	i32 2315684594, ; 352: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 210
	i32 2320631194, ; 353: System.Threading.Tasks.Parallel.dll => 0x8a52059a => 142
	i32 2340441535, ; 354: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 105
	i32 2344264397, ; 355: System.ValueTuple => 0x8bbaa2cd => 150
	i32 2353062107, ; 356: System.Net.Primitives => 0x8c40e0db => 69
	i32 2368005991, ; 357: System.Xml.ReaderWriter.dll => 0x8d24e767 => 155
	i32 2369706906, ; 358: Microsoft.IdentityModel.Logging => 0x8d3edb9a => 193
	i32 2371007202, ; 359: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 177
	i32 2378619854, ; 360: System.Security.Cryptography.Csp.dll => 0x8dc6dbce => 120
	i32 2383496789, ; 361: System.Security.Principal.Windows.dll => 0x8e114655 => 126
	i32 2395872292, ; 362: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 300
	i32 2401565422, ; 363: System.Web.HttpUtility => 0x8f24faee => 151
	i32 2403452196, ; 364: Xamarin.AndroidX.Emoji2.dll => 0x8f41c524 => 233
	i32 2409983638, ; 365: Microsoft.VisualStudio.DesignTools.MobileTapContracts.dll => 0x8fa56e96 => 322
	i32 2421380589, ; 366: System.Threading.Tasks.Dataflow => 0x905355ed => 140
	i32 2423080555, ; 367: Xamarin.AndroidX.Collection.Ktx.dll => 0x906d466b => 220
	i32 2427813419, ; 368: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 297
	i32 2435356389, ; 369: System.Console.dll => 0x912896e5 => 20
	i32 2435904999, ; 370: System.ComponentModel.DataAnnotations.dll => 0x9130f5e7 => 14
	i32 2454642406, ; 371: System.Text.Encoding.dll => 0x924edee6 => 134
	i32 2458678730, ; 372: System.Net.Sockets.dll => 0x928c75ca => 74
	i32 2459001652, ; 373: System.Linq.Parallel.dll => 0x92916334 => 58
	i32 2465532216, ; 374: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 223
	i32 2471841756, ; 375: netstandard.dll => 0x93554fdc => 166
	i32 2475788418, ; 376: Java.Interop.dll => 0x93918882 => 167
	i32 2480646305, ; 377: Microsoft.Maui.Controls => 0x93dba8a1 => 196
	i32 2483903535, ; 378: System.ComponentModel.EventBasedAsync => 0x940d5c2f => 15
	i32 2484371297, ; 379: System.Net.ServicePoint => 0x94147f61 => 73
	i32 2490993605, ; 380: System.AppContext.dll => 0x94798bc5 => 6
	i32 2501346920, ; 381: System.Data.DataSetExtensions => 0x95178668 => 23
	i32 2505896520, ; 382: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 245
	i32 2522472828, ; 383: Xamarin.Android.Glide.dll => 0x9659e17c => 204
	i32 2538310050, ; 384: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 90
	i32 2550873716, ; 385: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 298
	i32 2562349572, ; 386: Microsoft.CSharp => 0x98ba5a04 => 1
	i32 2570120770, ; 387: System.Text.Encodings.Web => 0x9930ee42 => 135
	i32 2581783588, ; 388: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 0x99e2e424 => 246
	i32 2581819634, ; 389: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 268
	i32 2585220780, ; 390: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 133
	i32 2585805581, ; 391: System.Net.Ping => 0x9a20430d => 68
	i32 2589602615, ; 392: System.Threading.ThreadPool => 0x9a5a3337 => 145
	i32 2593496499, ; 393: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 307
	i32 2605712449, ; 394: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 286
	i32 2615233544, ; 395: Xamarin.AndroidX.Fragment.Ktx => 0x9be14c08 => 237
	i32 2616218305, ; 396: Microsoft.Extensions.Logging.Debug.dll => 0x9bf052c1 => 187
	i32 2617129537, ; 397: System.Private.Xml.dll => 0x9bfe3a41 => 87
	i32 2618712057, ; 398: System.Reflection.TypeExtensions.dll => 0x9c165ff9 => 95
	i32 2620871830, ; 399: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 227
	i32 2624644809, ; 400: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 232
	i32 2626831493, ; 401: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 302
	i32 2627185994, ; 402: System.Diagnostics.TextWriterTraceListener.dll => 0x9c97ad4a => 30
	i32 2629843544, ; 403: System.IO.Compression.ZipFile.dll => 0x9cc03a58 => 44
	i32 2633051222, ; 404: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 241
	i32 2640290731, ; 405: Microsoft.IdentityModel.Logging.dll => 0x9d5fa3ab => 193
	i32 2663391936, ; 406: Xamarin.Android.Glide.DiskLruCache => 0x9ec022c0 => 206
	i32 2663698177, ; 407: System.Runtime.Loader => 0x9ec4cf01 => 108
	i32 2664396074, ; 408: System.Xml.XDocument.dll => 0x9ecf752a => 157
	i32 2665622720, ; 409: System.Drawing.Primitives => 0x9ee22cc0 => 34
	i32 2676780864, ; 410: System.Data.Common.dll => 0x9f8c6f40 => 22
	i32 2686887180, ; 411: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 113
	i32 2693849962, ; 412: System.IO.dll => 0xa090e36a => 56
	i32 2701096212, ; 413: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 266
	i32 2715334215, ; 414: System.Threading.Tasks.dll => 0xa1d8b647 => 143
	i32 2717744543, ; 415: System.Security.Claims => 0xa1fd7d9f => 117
	i32 2719963679, ; 416: System.Security.Cryptography.Cng.dll => 0xa21f5a1f => 119
	i32 2724373263, ; 417: System.Runtime.Numerics.dll => 0xa262a30f => 109
	i32 2732626843, ; 418: Xamarin.AndroidX.Activity => 0xa2e0939b => 208
	i32 2735172069, ; 419: System.Threading.Channels => 0xa30769e5 => 138
	i32 2737747696, ; 420: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 214
	i32 2740948882, ; 421: System.IO.Pipes.AccessControl => 0xa35f8f92 => 53
	i32 2748088231, ; 422: System.Runtime.InteropServices.JavaScript => 0xa3cc7fa7 => 104
	i32 2752995522, ; 423: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 308
	i32 2758225723, ; 424: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 197
	i32 2764765095, ; 425: Microsoft.Maui.dll => 0xa4caf7a7 => 198
	i32 2765824710, ; 426: System.Text.Encoding.CodePages.dll => 0xa4db22c6 => 132
	i32 2770495804, ; 427: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 280
	i32 2778768386, ; 428: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 271
	i32 2779977773, ; 429: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0xa5b3182d => 259
	i32 2785988530, ; 430: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 314
	i32 2788224221, ; 431: Xamarin.AndroidX.Fragment.Ktx.dll => 0xa630ecdd => 237
	i32 2801831435, ; 432: Microsoft.Maui.Graphics => 0xa7008e0b => 200
	i32 2803228030, ; 433: System.Xml.XPath.XDocument.dll => 0xa715dd7e => 158
	i32 2806116107, ; 434: es/Microsoft.Maui.Controls.resources.dll => 0xa741ef0b => 293
	i32 2810250172, ; 435: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 224
	i32 2819470561, ; 436: System.Xml.dll => 0xa80db4e1 => 162
	i32 2821205001, ; 437: System.ServiceProcess.dll => 0xa8282c09 => 131
	i32 2821294376, ; 438: Xamarin.AndroidX.ResourceInspection.Annotation => 0xa8298928 => 259
	i32 2824502124, ; 439: System.Xml.XmlDocument => 0xa85a7b6c => 160
	i32 2831556043, ; 440: nl/Microsoft.Maui.Controls.resources.dll => 0xa8c61dcb => 306
	i32 2838993487, ; 441: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 0xa9379a4f => 248
	i32 2849599387, ; 442: System.Threading.Overlapped.dll => 0xa9d96f9b => 139
	i32 2853208004, ; 443: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 271
	i32 2855708567, ; 444: Xamarin.AndroidX.Transition => 0xaa36a797 => 267
	i32 2861098320, ; 445: Mono.Android.Export.dll => 0xaa88e550 => 168
	i32 2861189240, ; 446: Microsoft.Maui.Essentials => 0xaa8a4878 => 199
	i32 2868488919, ; 447: CommunityToolkit.Maui.Core => 0xaaf9aad7 => 173
	i32 2870099610, ; 448: Xamarin.AndroidX.Activity.Ktx.dll => 0xab123e9a => 209
	i32 2875164099, ; 449: Jsr305Binding.dll => 0xab5f85c3 => 276
	i32 2875220617, ; 450: System.Globalization.Calendars.dll => 0xab606289 => 39
	i32 2884993177, ; 451: Xamarin.AndroidX.ExifInterface => 0xabf58099 => 235
	i32 2887636118, ; 452: System.Net.dll => 0xac1dd496 => 80
	i32 2899753641, ; 453: System.IO.UnmanagedMemoryStream => 0xacd6baa9 => 55
	i32 2900621748, ; 454: System.Dynamic.Runtime.dll => 0xace3f9b4 => 36
	i32 2901442782, ; 455: System.Reflection => 0xacf080de => 96
	i32 2905242038, ; 456: mscorlib.dll => 0xad2a79b6 => 165
	i32 2909740682, ; 457: System.Private.CoreLib => 0xad6f1e8a => 171
	i32 2916838712, ; 458: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 272
	i32 2919462931, ; 459: System.Numerics.Vectors.dll => 0xae037813 => 81
	i32 2921128767, ; 460: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 211
	i32 2936416060, ; 461: System.Resources.Reader => 0xaf06273c => 97
	i32 2940926066, ; 462: System.Diagnostics.StackTrace.dll => 0xaf4af872 => 29
	i32 2942453041, ; 463: System.Xml.XPath.XDocument => 0xaf624531 => 158
	i32 2959614098, ; 464: System.ComponentModel.dll => 0xb0682092 => 18
	i32 2968338931, ; 465: System.Security.Principal.Windows => 0xb0ed41f3 => 126
	i32 2971004615, ; 466: Microsoft.Extensions.Options.ConfigurationExtensions.dll => 0xb115eec7 => 189
	i32 2972252294, ; 467: System.Security.Cryptography.Algorithms.dll => 0xb128f886 => 118
	i32 2978675010, ; 468: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 231
	i32 2987532451, ; 469: Xamarin.AndroidX.Security.SecurityCrypto => 0xb21220a3 => 262
	i32 2996846495, ; 470: Xamarin.AndroidX.Lifecycle.Process.dll => 0xb2a03f9f => 244
	i32 3016983068, ; 471: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 264
	i32 3020703001, ; 472: Microsoft.Extensions.Diagnostics => 0xb40c4519 => 182
	i32 3023353419, ; 473: WindowsBase.dll => 0xb434b64b => 164
	i32 3024354802, ; 474: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 239
	i32 3038032645, ; 475: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 324
	i32 3056245963, ; 476: Xamarin.AndroidX.SavedState.SavedState.Ktx => 0xb62a9ccb => 261
	i32 3057625584, ; 477: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 252
	i32 3059408633, ; 478: Mono.Android.Runtime => 0xb65adef9 => 169
	i32 3059793426, ; 479: System.ComponentModel.Primitives => 0xb660be12 => 16
	i32 3075834255, ; 480: System.Threading.Tasks => 0xb755818f => 143
	i32 3077302341, ; 481: hu/Microsoft.Maui.Controls.resources.dll => 0xb76be845 => 299
	i32 3084678329, ; 482: Microsoft.IdentityModel.Tokens => 0xb7dc74b9 => 194
	i32 3090735792, ; 483: System.Security.Cryptography.X509Certificates.dll => 0xb838e2b0 => 124
	i32 3099732863, ; 484: System.Security.Claims.dll => 0xb8c22b7f => 117
	i32 3103600923, ; 485: System.Formats.Asn1 => 0xb8fd311b => 37
	i32 3111772706, ; 486: System.Runtime.Serialization => 0xb979e222 => 114
	i32 3121463068, ; 487: System.IO.FileSystem.AccessControl.dll => 0xba0dbf1c => 46
	i32 3124832203, ; 488: System.Threading.Tasks.Extensions => 0xba4127cb => 141
	i32 3132293585, ; 489: System.Security.AccessControl => 0xbab301d1 => 116
	i32 3147165239, ; 490: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 33
	i32 3148237826, ; 491: GoogleGson.dll => 0xbba64c02 => 175
	i32 3159123045, ; 492: System.Reflection.Primitives.dll => 0xbc4c6465 => 94
	i32 3160747431, ; 493: System.IO.MemoryMappedFiles => 0xbc652da7 => 52
	i32 3178803400, ; 494: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 253
	i32 3192346100, ; 495: System.Security.SecureString => 0xbe4755f4 => 128
	i32 3193515020, ; 496: System.Web => 0xbe592c0c => 152
	i32 3204380047, ; 497: System.Data.dll => 0xbefef58f => 24
	i32 3209718065, ; 498: System.Xml.XmlDocument.dll => 0xbf506931 => 160
	i32 3211777861, ; 499: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 230
	i32 3220365878, ; 500: System.Threading => 0xbff2e236 => 147
	i32 3226221578, ; 501: System.Runtime.Handles.dll => 0xc04c3c0a => 103
	i32 3251039220, ; 502: System.Reflection.DispatchProxy.dll => 0xc1c6ebf4 => 88
	i32 3258312781, ; 503: Xamarin.AndroidX.CardView => 0xc235e84d => 218
	i32 3265493905, ; 504: System.Linq.Queryable.dll => 0xc2a37b91 => 59
	i32 3265893370, ; 505: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 141
	i32 3277815716, ; 506: System.Resources.Writer.dll => 0xc35f7fa4 => 99
	i32 3279906254, ; 507: Microsoft.Win32.Registry.dll => 0xc37f65ce => 5
	i32 3280506390, ; 508: System.ComponentModel.Annotations.dll => 0xc3888e16 => 13
	i32 3290767353, ; 509: System.Security.Cryptography.Encoding => 0xc4251ff9 => 121
	i32 3299363146, ; 510: System.Text.Encoding => 0xc4a8494a => 134
	i32 3303498502, ; 511: System.Diagnostics.FileVersionInfo => 0xc4e76306 => 27
	i32 3305363605, ; 512: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 294
	i32 3312457198, ; 513: Microsoft.IdentityModel.JsonWebTokens => 0xc57015ee => 192
	i32 3316684772, ; 514: System.Net.Requests.dll => 0xc5b097e4 => 71
	i32 3317135071, ; 515: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 228
	i32 3317144872, ; 516: System.Data => 0xc5b79d28 => 24
	i32 3340431453, ; 517: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 216
	i32 3345895724, ; 518: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 257
	i32 3346324047, ; 519: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 254
	i32 3357674450, ; 520: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 311
	i32 3358260929, ; 521: System.Text.Json => 0xc82afec1 => 136
	i32 3362336904, ; 522: Xamarin.AndroidX.Activity.Ktx => 0xc8693088 => 209
	i32 3362522851, ; 523: Xamarin.AndroidX.Core => 0xc86c06e3 => 225
	i32 3366347497, ; 524: Java.Interop => 0xc8a662e9 => 167
	i32 3374999561, ; 525: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 258
	i32 3381016424, ; 526: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 290
	i32 3395150330, ; 527: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 100
	i32 3403906625, ; 528: System.Security.Cryptography.OpenSsl.dll => 0xcae37e41 => 122
	i32 3405233483, ; 529: Xamarin.AndroidX.CustomView.PoolingContainer => 0xcaf7bd4b => 229
	i32 3421170118, ; 530: Microsoft.Extensions.Configuration.Binder => 0xcbeae9c6 => 179
	i32 3428513518, ; 531: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 180
	i32 3429136800, ; 532: System.Xml => 0xcc6479a0 => 162
	i32 3430777524, ; 533: netstandard => 0xcc7d82b4 => 166
	i32 3441283291, ; 534: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 232
	i32 3445260447, ; 535: System.Formats.Tar => 0xcd5a809f => 38
	i32 3452344032, ; 536: Microsoft.Maui.Controls.Compatibility.dll => 0xcdc696e0 => 195
	i32 3463511458, ; 537: hr/Microsoft.Maui.Controls.resources.dll => 0xce70fda2 => 298
	i32 3471940407, ; 538: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 17
	i32 3476120550, ; 539: Mono.Android => 0xcf3163e6 => 170
	i32 3479583265, ; 540: ru/Microsoft.Maui.Controls.resources.dll => 0xcf663a21 => 311
	i32 3484440000, ; 541: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 310
	i32 3485117614, ; 542: System.Text.Json.dll => 0xcfbaacae => 136
	i32 3486566296, ; 543: System.Transactions => 0xcfd0c798 => 149
	i32 3493954962, ; 544: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 221
	i32 3509114376, ; 545: System.Xml.Linq => 0xd128d608 => 154
	i32 3515174580, ; 546: System.Security.dll => 0xd1854eb4 => 129
	i32 3530912306, ; 547: System.Configuration => 0xd2757232 => 19
	i32 3539954161, ; 548: System.Net.HttpListener => 0xd2ff69f1 => 64
	i32 3560100363, ; 549: System.Threading.Timer => 0xd432d20b => 146
	i32 3570554715, ; 550: System.IO.FileSystem.AccessControl => 0xd4d2575b => 46
	i32 3580758918, ; 551: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 318
	i32 3597029428, ; 552: Xamarin.Android.Glide.GifDecoder.dll => 0xd6665034 => 207
	i32 3598340787, ; 553: System.Net.WebSockets.Client => 0xd67a52b3 => 78
	i32 3608519521, ; 554: System.Linq.dll => 0xd715a361 => 60
	i32 3624195450, ; 555: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 105
	i32 3627220390, ; 556: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 256
	i32 3633644679, ; 557: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 211
	i32 3638274909, ; 558: System.IO.FileSystem.Primitives.dll => 0xd8dbab5d => 48
	i32 3641597786, ; 559: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 242
	i32 3643446276, ; 560: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 315
	i32 3643854240, ; 561: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 253
	i32 3645089577, ; 562: System.ComponentModel.DataAnnotations => 0xd943a729 => 14
	i32 3657292374, ; 563: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 178
	i32 3660523487, ; 564: System.Net.NetworkInformation => 0xda2f27df => 67
	i32 3672681054, ; 565: Mono.Android.dll => 0xdae8aa5e => 170
	i32 3676670898, ; 566: Microsoft.Maui.Controls.HotReload.Forms => 0xdb258bb2 => 321
	i32 3682565725, ; 567: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 217
	i32 3684561358, ; 568: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 221
	i32 3697841164, ; 569: zh-Hant/Microsoft.Maui.Controls.resources.dll => 0xdc68940c => 320
	i32 3700591436, ; 570: Microsoft.IdentityModel.Abstractions.dll => 0xdc928b4c => 191
	i32 3700866549, ; 571: System.Net.WebProxy.dll => 0xdc96bdf5 => 77
	i32 3706696989, ; 572: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 226
	i32 3716563718, ; 573: System.Runtime.Intrinsics => 0xdd864306 => 107
	i32 3718780102, ; 574: Xamarin.AndroidX.Annotation => 0xdda814c6 => 210
	i32 3724971120, ; 575: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 252
	i32 3732100267, ; 576: System.Net.NameResolution => 0xde7354ab => 66
	i32 3737834244, ; 577: System.Net.Http.Json.dll => 0xdecad304 => 62
	i32 3748608112, ; 578: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 202
	i32 3751444290, ; 579: System.Xml.XPath => 0xdf9a7f42 => 159
	i32 3786282454, ; 580: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 219
	i32 3792276235, ; 581: System.Collections.NonGeneric => 0xe2098b0b => 10
	i32 3800979733, ; 582: Microsoft.Maui.Controls.Compatibility => 0xe28e5915 => 195
	i32 3802395368, ; 583: System.Collections.Specialized.dll => 0xe2a3f2e8 => 11
	i32 3817368567, ; 584: CommunityToolkit.Maui.dll => 0xe3886bf7 => 172
	i32 3819260425, ; 585: System.Net.WebProxy => 0xe3a54a09 => 77
	i32 3823082795, ; 586: System.Security.Cryptography.dll => 0xe3df9d2b => 125
	i32 3829621856, ; 587: System.Numerics.dll => 0xe4436460 => 82
	i32 3841636137, ; 588: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 181
	i32 3844307129, ; 589: System.Net.Mail.dll => 0xe52378b9 => 65
	i32 3849253459, ; 590: System.Runtime.InteropServices.dll => 0xe56ef253 => 106
	i32 3870376305, ; 591: System.Net.HttpListener.dll => 0xe6b14171 => 64
	i32 3873536506, ; 592: System.Security.Principal => 0xe6e179fa => 127
	i32 3875112723, ; 593: System.Security.Cryptography.Encoding.dll => 0xe6f98713 => 121
	i32 3885497537, ; 594: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 76
	i32 3885922214, ; 595: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 267
	i32 3888767677, ; 596: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 257
	i32 3889960447, ; 597: zh-Hans/Microsoft.Maui.Controls.resources.dll => 0xe7dc15ff => 319
	i32 3896106733, ; 598: System.Collections.Concurrent.dll => 0xe839deed => 8
	i32 3896760992, ; 599: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 225
	i32 3901907137, ; 600: Microsoft.VisualBasic.Core.dll => 0xe89260c1 => 2
	i32 3920810846, ; 601: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 43
	i32 3921031405, ; 602: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 270
	i32 3928044579, ; 603: System.Xml.ReaderWriter => 0xea213423 => 155
	i32 3930554604, ; 604: System.Security.Principal.dll => 0xea4780ec => 127
	i32 3931092270, ; 605: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 255
	i32 3945713374, ; 606: System.Data.DataSetExtensions.dll => 0xeb2ecede => 23
	i32 3953953790, ; 607: System.Text.Encoding.CodePages => 0xebac8bfe => 132
	i32 3955647286, ; 608: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 213
	i32 3959773229, ; 609: Xamarin.AndroidX.Lifecycle.Process => 0xec05582d => 244
	i32 3980434154, ; 610: th/Microsoft.Maui.Controls.resources.dll => 0xed409aea => 314
	i32 3987592930, ; 611: he/Microsoft.Maui.Controls.resources.dll => 0xedadd6e2 => 296
	i32 4003436829, ; 612: System.Diagnostics.Process.dll => 0xee9f991d => 28
	i32 4015948917, ; 613: Xamarin.AndroidX.Annotation.Jvm.dll => 0xef5e8475 => 212
	i32 4019855520, ; 614: DigesettAPP.dll => 0xef9a20a0 => 0
	i32 4025784931, ; 615: System.Memory => 0xeff49a63 => 61
	i32 4046471985, ; 616: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 197
	i32 4054681211, ; 617: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 89
	i32 4068434129, ; 618: System.Private.Xml.Linq.dll => 0xf27f60d1 => 86
	i32 4073602200, ; 619: System.Threading.dll => 0xf2ce3c98 => 147
	i32 4094352644, ; 620: Microsoft.Maui.Essentials.dll => 0xf40add04 => 199
	i32 4099507663, ; 621: System.Drawing.dll => 0xf45985cf => 35
	i32 4100113165, ; 622: System.Private.Uri => 0xf462c30d => 85
	i32 4101593132, ; 623: Xamarin.AndroidX.Emoji2 => 0xf479582c => 233
	i32 4102112229, ; 624: pt/Microsoft.Maui.Controls.resources.dll => 0xf48143e5 => 309
	i32 4125707920, ; 625: ms/Microsoft.Maui.Controls.resources.dll => 0xf5e94e90 => 304
	i32 4126470640, ; 626: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 180
	i32 4127667938, ; 627: System.IO.FileSystem.Watcher => 0xf60736e2 => 49
	i32 4130442656, ; 628: System.AppContext => 0xf6318da0 => 6
	i32 4147896353, ; 629: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 89
	i32 4150914736, ; 630: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 316
	i32 4151237749, ; 631: System.Core => 0xf76edc75 => 21
	i32 4159265925, ; 632: System.Xml.XmlSerializer => 0xf7e95c85 => 161
	i32 4161255271, ; 633: System.Reflection.TypeExtensions => 0xf807b767 => 95
	i32 4164802419, ; 634: System.IO.FileSystem.Watcher.dll => 0xf83dd773 => 49
	i32 4181436372, ; 635: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 112
	i32 4182413190, ; 636: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 249
	i32 4182880526, ; 637: Microsoft.VisualStudio.DesignTools.MobileTapContracts => 0xf951b10e => 322
	i32 4185676441, ; 638: System.Security => 0xf97c5a99 => 129
	i32 4196529839, ; 639: System.Net.WebClient.dll => 0xfa21f6af => 75
	i32 4213026141, ; 640: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 202
	i32 4256097574, ; 641: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 226
	i32 4258378803, ; 642: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 0xfdd1b433 => 248
	i32 4260525087, ; 643: System.Buffers => 0xfdf2741f => 7
	i32 4263231520, ; 644: System.IdentityModel.Tokens.Jwt.dll => 0xfe1bc020 => 203
	i32 4271975918, ; 645: Microsoft.Maui.Controls.dll => 0xfea12dee => 196
	i32 4274623895, ; 646: CommunityToolkit.Mvvm.dll => 0xfec99597 => 174
	i32 4274976490, ; 647: System.Runtime.Numerics => 0xfecef6ea => 109
	i32 4292120959, ; 648: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 249
	i32 4294763496 ; 649: Xamarin.AndroidX.ExifInterface.dll => 0xfffce3e8 => 235
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [650 x i32] [
	i32 67, ; 0
	i32 66, ; 1
	i32 107, ; 2
	i32 245, ; 3
	i32 279, ; 4
	i32 47, ; 5
	i32 201, ; 6
	i32 79, ; 7
	i32 144, ; 8
	i32 29, ; 9
	i32 320, ; 10
	i32 123, ; 11
	i32 200, ; 12
	i32 101, ; 13
	i32 183, ; 14
	i32 263, ; 15
	i32 106, ; 16
	i32 263, ; 17
	i32 138, ; 18
	i32 283, ; 19
	i32 76, ; 20
	i32 123, ; 21
	i32 13, ; 22
	i32 219, ; 23
	i32 131, ; 24
	i32 265, ; 25
	i32 150, ; 26
	i32 317, ; 27
	i32 318, ; 28
	i32 18, ; 29
	i32 217, ; 30
	i32 26, ; 31
	i32 182, ; 32
	i32 239, ; 33
	i32 1, ; 34
	i32 58, ; 35
	i32 41, ; 36
	i32 90, ; 37
	i32 222, ; 38
	i32 146, ; 39
	i32 241, ; 40
	i32 238, ; 41
	i32 289, ; 42
	i32 53, ; 43
	i32 184, ; 44
	i32 68, ; 45
	i32 317, ; 46
	i32 208, ; 47
	i32 82, ; 48
	i32 302, ; 49
	i32 240, ; 50
	i32 301, ; 51
	i32 130, ; 52
	i32 54, ; 53
	i32 148, ; 54
	i32 73, ; 55
	i32 144, ; 56
	i32 176, ; 57
	i32 61, ; 58
	i32 145, ; 59
	i32 324, ; 60
	i32 164, ; 61
	i32 313, ; 62
	i32 223, ; 63
	i32 12, ; 64
	i32 236, ; 65
	i32 124, ; 66
	i32 151, ; 67
	i32 112, ; 68
	i32 165, ; 69
	i32 163, ; 70
	i32 238, ; 71
	i32 191, ; 72
	i32 251, ; 73
	i32 83, ; 74
	i32 300, ; 75
	i32 294, ; 76
	i32 190, ; 77
	i32 149, ; 78
	i32 283, ; 79
	i32 59, ; 80
	i32 185, ; 81
	i32 50, ; 82
	i32 102, ; 83
	i32 113, ; 84
	i32 39, ; 85
	i32 276, ; 86
	i32 274, ; 87
	i32 119, ; 88
	i32 308, ; 89
	i32 172, ; 90
	i32 51, ; 91
	i32 43, ; 92
	i32 118, ; 93
	i32 228, ; 94
	i32 306, ; 95
	i32 234, ; 96
	i32 80, ; 97
	i32 135, ; 98
	i32 270, ; 99
	i32 215, ; 100
	i32 8, ; 101
	i32 72, ; 102
	i32 288, ; 103
	i32 154, ; 104
	i32 285, ; 105
	i32 153, ; 106
	i32 91, ; 107
	i32 280, ; 108
	i32 44, ; 109
	i32 303, ; 110
	i32 291, ; 111
	i32 284, ; 112
	i32 108, ; 113
	i32 189, ; 114
	i32 128, ; 115
	i32 25, ; 116
	i32 205, ; 117
	i32 71, ; 118
	i32 54, ; 119
	i32 45, ; 120
	i32 312, ; 121
	i32 188, ; 122
	i32 229, ; 123
	i32 22, ; 124
	i32 243, ; 125
	i32 85, ; 126
	i32 42, ; 127
	i32 159, ; 128
	i32 70, ; 129
	i32 256, ; 130
	i32 3, ; 131
	i32 41, ; 132
	i32 62, ; 133
	i32 16, ; 134
	i32 52, ; 135
	i32 315, ; 136
	i32 279, ; 137
	i32 104, ; 138
	i32 201, ; 139
	i32 284, ; 140
	i32 277, ; 141
	i32 240, ; 142
	i32 33, ; 143
	i32 157, ; 144
	i32 84, ; 145
	i32 31, ; 146
	i32 12, ; 147
	i32 50, ; 148
	i32 55, ; 149
	i32 260, ; 150
	i32 35, ; 151
	i32 181, ; 152
	i32 290, ; 153
	i32 278, ; 154
	i32 213, ; 155
	i32 34, ; 156
	i32 57, ; 157
	i32 183, ; 158
	i32 247, ; 159
	i32 175, ; 160
	i32 17, ; 161
	i32 281, ; 162
	i32 163, ; 163
	i32 303, ; 164
	i32 246, ; 165
	i32 187, ; 166
	i32 273, ; 167
	i32 176, ; 168
	i32 309, ; 169
	i32 152, ; 170
	i32 269, ; 171
	i32 254, ; 172
	i32 307, ; 173
	i32 215, ; 174
	i32 28, ; 175
	i32 174, ; 176
	i32 51, ; 177
	i32 305, ; 178
	i32 274, ; 179
	i32 5, ; 180
	i32 289, ; 181
	i32 264, ; 182
	i32 268, ; 183
	i32 220, ; 184
	i32 285, ; 185
	i32 212, ; 186
	i32 231, ; 187
	i32 84, ; 188
	i32 273, ; 189
	i32 60, ; 190
	i32 111, ; 191
	i32 56, ; 192
	i32 319, ; 193
	i32 260, ; 194
	i32 98, ; 195
	i32 19, ; 196
	i32 224, ; 197
	i32 110, ; 198
	i32 100, ; 199
	i32 101, ; 200
	i32 287, ; 201
	i32 103, ; 202
	i32 277, ; 203
	i32 70, ; 204
	i32 37, ; 205
	i32 31, ; 206
	i32 102, ; 207
	i32 72, ; 208
	i32 203, ; 209
	i32 293, ; 210
	i32 9, ; 211
	i32 122, ; 212
	i32 45, ; 213
	i32 214, ; 214
	i32 190, ; 215
	i32 9, ; 216
	i32 42, ; 217
	i32 4, ; 218
	i32 261, ; 219
	i32 297, ; 220
	i32 192, ; 221
	i32 184, ; 222
	i32 292, ; 223
	i32 30, ; 224
	i32 137, ; 225
	i32 91, ; 226
	i32 92, ; 227
	i32 312, ; 228
	i32 48, ; 229
	i32 140, ; 230
	i32 111, ; 231
	i32 139, ; 232
	i32 230, ; 233
	i32 114, ; 234
	i32 278, ; 235
	i32 156, ; 236
	i32 321, ; 237
	i32 75, ; 238
	i32 78, ; 239
	i32 250, ; 240
	i32 36, ; 241
	i32 272, ; 242
	i32 173, ; 243
	i32 234, ; 244
	i32 227, ; 245
	i32 63, ; 246
	i32 137, ; 247
	i32 15, ; 248
	i32 115, ; 249
	i32 266, ; 250
	i32 275, ; 251
	i32 222, ; 252
	i32 47, ; 253
	i32 69, ; 254
	i32 79, ; 255
	i32 125, ; 256
	i32 93, ; 257
	i32 120, ; 258
	i32 282, ; 259
	i32 26, ; 260
	i32 243, ; 261
	i32 96, ; 262
	i32 27, ; 263
	i32 218, ; 264
	i32 310, ; 265
	i32 288, ; 266
	i32 148, ; 267
	i32 168, ; 268
	i32 4, ; 269
	i32 97, ; 270
	i32 32, ; 271
	i32 92, ; 272
	i32 265, ; 273
	i32 185, ; 274
	i32 21, ; 275
	i32 40, ; 276
	i32 169, ; 277
	i32 304, ; 278
	i32 236, ; 279
	i32 296, ; 280
	i32 250, ; 281
	i32 281, ; 282
	i32 275, ; 283
	i32 255, ; 284
	i32 2, ; 285
	i32 133, ; 286
	i32 110, ; 287
	i32 323, ; 288
	i32 0, ; 289
	i32 186, ; 290
	i32 316, ; 291
	i32 205, ; 292
	i32 313, ; 293
	i32 57, ; 294
	i32 94, ; 295
	i32 295, ; 296
	i32 38, ; 297
	i32 216, ; 298
	i32 323, ; 299
	i32 25, ; 300
	i32 93, ; 301
	i32 88, ; 302
	i32 98, ; 303
	i32 10, ; 304
	i32 86, ; 305
	i32 99, ; 306
	i32 262, ; 307
	i32 177, ; 308
	i32 282, ; 309
	i32 207, ; 310
	i32 194, ; 311
	i32 292, ; 312
	i32 7, ; 313
	i32 247, ; 314
	i32 287, ; 315
	i32 204, ; 316
	i32 87, ; 317
	i32 179, ; 318
	i32 242, ; 319
	i32 153, ; 320
	i32 291, ; 321
	i32 32, ; 322
	i32 115, ; 323
	i32 81, ; 324
	i32 20, ; 325
	i32 11, ; 326
	i32 161, ; 327
	i32 3, ; 328
	i32 198, ; 329
	i32 299, ; 330
	i32 188, ; 331
	i32 186, ; 332
	i32 83, ; 333
	i32 286, ; 334
	i32 63, ; 335
	i32 301, ; 336
	i32 269, ; 337
	i32 142, ; 338
	i32 251, ; 339
	i32 156, ; 340
	i32 40, ; 341
	i32 116, ; 342
	i32 178, ; 343
	i32 206, ; 344
	i32 295, ; 345
	i32 258, ; 346
	i32 130, ; 347
	i32 74, ; 348
	i32 65, ; 349
	i32 305, ; 350
	i32 171, ; 351
	i32 210, ; 352
	i32 142, ; 353
	i32 105, ; 354
	i32 150, ; 355
	i32 69, ; 356
	i32 155, ; 357
	i32 193, ; 358
	i32 177, ; 359
	i32 120, ; 360
	i32 126, ; 361
	i32 300, ; 362
	i32 151, ; 363
	i32 233, ; 364
	i32 322, ; 365
	i32 140, ; 366
	i32 220, ; 367
	i32 297, ; 368
	i32 20, ; 369
	i32 14, ; 370
	i32 134, ; 371
	i32 74, ; 372
	i32 58, ; 373
	i32 223, ; 374
	i32 166, ; 375
	i32 167, ; 376
	i32 196, ; 377
	i32 15, ; 378
	i32 73, ; 379
	i32 6, ; 380
	i32 23, ; 381
	i32 245, ; 382
	i32 204, ; 383
	i32 90, ; 384
	i32 298, ; 385
	i32 1, ; 386
	i32 135, ; 387
	i32 246, ; 388
	i32 268, ; 389
	i32 133, ; 390
	i32 68, ; 391
	i32 145, ; 392
	i32 307, ; 393
	i32 286, ; 394
	i32 237, ; 395
	i32 187, ; 396
	i32 87, ; 397
	i32 95, ; 398
	i32 227, ; 399
	i32 232, ; 400
	i32 302, ; 401
	i32 30, ; 402
	i32 44, ; 403
	i32 241, ; 404
	i32 193, ; 405
	i32 206, ; 406
	i32 108, ; 407
	i32 157, ; 408
	i32 34, ; 409
	i32 22, ; 410
	i32 113, ; 411
	i32 56, ; 412
	i32 266, ; 413
	i32 143, ; 414
	i32 117, ; 415
	i32 119, ; 416
	i32 109, ; 417
	i32 208, ; 418
	i32 138, ; 419
	i32 214, ; 420
	i32 53, ; 421
	i32 104, ; 422
	i32 308, ; 423
	i32 197, ; 424
	i32 198, ; 425
	i32 132, ; 426
	i32 280, ; 427
	i32 271, ; 428
	i32 259, ; 429
	i32 314, ; 430
	i32 237, ; 431
	i32 200, ; 432
	i32 158, ; 433
	i32 293, ; 434
	i32 224, ; 435
	i32 162, ; 436
	i32 131, ; 437
	i32 259, ; 438
	i32 160, ; 439
	i32 306, ; 440
	i32 248, ; 441
	i32 139, ; 442
	i32 271, ; 443
	i32 267, ; 444
	i32 168, ; 445
	i32 199, ; 446
	i32 173, ; 447
	i32 209, ; 448
	i32 276, ; 449
	i32 39, ; 450
	i32 235, ; 451
	i32 80, ; 452
	i32 55, ; 453
	i32 36, ; 454
	i32 96, ; 455
	i32 165, ; 456
	i32 171, ; 457
	i32 272, ; 458
	i32 81, ; 459
	i32 211, ; 460
	i32 97, ; 461
	i32 29, ; 462
	i32 158, ; 463
	i32 18, ; 464
	i32 126, ; 465
	i32 189, ; 466
	i32 118, ; 467
	i32 231, ; 468
	i32 262, ; 469
	i32 244, ; 470
	i32 264, ; 471
	i32 182, ; 472
	i32 164, ; 473
	i32 239, ; 474
	i32 324, ; 475
	i32 261, ; 476
	i32 252, ; 477
	i32 169, ; 478
	i32 16, ; 479
	i32 143, ; 480
	i32 299, ; 481
	i32 194, ; 482
	i32 124, ; 483
	i32 117, ; 484
	i32 37, ; 485
	i32 114, ; 486
	i32 46, ; 487
	i32 141, ; 488
	i32 116, ; 489
	i32 33, ; 490
	i32 175, ; 491
	i32 94, ; 492
	i32 52, ; 493
	i32 253, ; 494
	i32 128, ; 495
	i32 152, ; 496
	i32 24, ; 497
	i32 160, ; 498
	i32 230, ; 499
	i32 147, ; 500
	i32 103, ; 501
	i32 88, ; 502
	i32 218, ; 503
	i32 59, ; 504
	i32 141, ; 505
	i32 99, ; 506
	i32 5, ; 507
	i32 13, ; 508
	i32 121, ; 509
	i32 134, ; 510
	i32 27, ; 511
	i32 294, ; 512
	i32 192, ; 513
	i32 71, ; 514
	i32 228, ; 515
	i32 24, ; 516
	i32 216, ; 517
	i32 257, ; 518
	i32 254, ; 519
	i32 311, ; 520
	i32 136, ; 521
	i32 209, ; 522
	i32 225, ; 523
	i32 167, ; 524
	i32 258, ; 525
	i32 290, ; 526
	i32 100, ; 527
	i32 122, ; 528
	i32 229, ; 529
	i32 179, ; 530
	i32 180, ; 531
	i32 162, ; 532
	i32 166, ; 533
	i32 232, ; 534
	i32 38, ; 535
	i32 195, ; 536
	i32 298, ; 537
	i32 17, ; 538
	i32 170, ; 539
	i32 311, ; 540
	i32 310, ; 541
	i32 136, ; 542
	i32 149, ; 543
	i32 221, ; 544
	i32 154, ; 545
	i32 129, ; 546
	i32 19, ; 547
	i32 64, ; 548
	i32 146, ; 549
	i32 46, ; 550
	i32 318, ; 551
	i32 207, ; 552
	i32 78, ; 553
	i32 60, ; 554
	i32 105, ; 555
	i32 256, ; 556
	i32 211, ; 557
	i32 48, ; 558
	i32 242, ; 559
	i32 315, ; 560
	i32 253, ; 561
	i32 14, ; 562
	i32 178, ; 563
	i32 67, ; 564
	i32 170, ; 565
	i32 321, ; 566
	i32 217, ; 567
	i32 221, ; 568
	i32 320, ; 569
	i32 191, ; 570
	i32 77, ; 571
	i32 226, ; 572
	i32 107, ; 573
	i32 210, ; 574
	i32 252, ; 575
	i32 66, ; 576
	i32 62, ; 577
	i32 202, ; 578
	i32 159, ; 579
	i32 219, ; 580
	i32 10, ; 581
	i32 195, ; 582
	i32 11, ; 583
	i32 172, ; 584
	i32 77, ; 585
	i32 125, ; 586
	i32 82, ; 587
	i32 181, ; 588
	i32 65, ; 589
	i32 106, ; 590
	i32 64, ; 591
	i32 127, ; 592
	i32 121, ; 593
	i32 76, ; 594
	i32 267, ; 595
	i32 257, ; 596
	i32 319, ; 597
	i32 8, ; 598
	i32 225, ; 599
	i32 2, ; 600
	i32 43, ; 601
	i32 270, ; 602
	i32 155, ; 603
	i32 127, ; 604
	i32 255, ; 605
	i32 23, ; 606
	i32 132, ; 607
	i32 213, ; 608
	i32 244, ; 609
	i32 314, ; 610
	i32 296, ; 611
	i32 28, ; 612
	i32 212, ; 613
	i32 0, ; 614
	i32 61, ; 615
	i32 197, ; 616
	i32 89, ; 617
	i32 86, ; 618
	i32 147, ; 619
	i32 199, ; 620
	i32 35, ; 621
	i32 85, ; 622
	i32 233, ; 623
	i32 309, ; 624
	i32 304, ; 625
	i32 180, ; 626
	i32 49, ; 627
	i32 6, ; 628
	i32 89, ; 629
	i32 316, ; 630
	i32 21, ; 631
	i32 161, ; 632
	i32 95, ; 633
	i32 49, ; 634
	i32 112, ; 635
	i32 249, ; 636
	i32 322, ; 637
	i32 129, ; 638
	i32 75, ; 639
	i32 202, ; 640
	i32 226, ; 641
	i32 248, ; 642
	i32 7, ; 643
	i32 203, ; 644
	i32 196, ; 645
	i32 174, ; 646
	i32 109, ; 647
	i32 249, ; 648
	i32 235 ; 649
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" }

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.4xx @ df9aaf29a52042a4fbf800daf2f3a38964b9e958"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"NumRegisterParameters", i32 0}
