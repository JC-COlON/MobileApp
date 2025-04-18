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

@assembly_image_cache = dso_local local_unnamed_addr global [336 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [666 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 67
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 66
	i32 15721112, ; 2: System.Runtime.Intrinsics.dll => 0xefe298 => 107
	i32 32687329, ; 3: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 252
	i32 34715100, ; 4: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 286
	i32 34839235, ; 5: System.IO.FileSystem.DriveInfo => 0x2139ac3 => 47
	i32 39109920, ; 6: Newtonsoft.Json.dll => 0x254c520 => 201
	i32 39485524, ; 7: System.Net.WebSockets.dll => 0x25a8054 => 79
	i32 42639949, ; 8: System.Threading.Thread => 0x28aa24d => 144
	i32 66541672, ; 9: System.Diagnostics.StackTrace => 0x3f75868 => 29
	i32 67008169, ; 10: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 327
	i32 68219467, ; 11: System.Security.Cryptography.Primitives => 0x410f24b => 123
	i32 72070932, ; 12: Microsoft.Maui.Graphics.dll => 0x44bb714 => 200
	i32 82292897, ; 13: System.Runtime.CompilerServices.VisualC.dll => 0x4e7b0a1 => 101
	i32 98325684, ; 14: Microsoft.Extensions.Diagnostics.Abstractions => 0x5dc54b4 => 183
	i32 101534019, ; 15: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 270
	i32 117431740, ; 16: System.Runtime.InteropServices => 0x6ffddbc => 106
	i32 120558881, ; 17: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 270
	i32 122350210, ; 18: System.Threading.Channels.dll => 0x74aea82 => 138
	i32 134690465, ; 19: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 290
	i32 142721839, ; 20: System.Net.WebHeaderCollection => 0x881c32f => 76
	i32 149972175, ; 21: System.Security.Cryptography.Primitives.dll => 0x8f064cf => 123
	i32 159306688, ; 22: System.ComponentModel.Annotations => 0x97ed3c0 => 13
	i32 165246403, ; 23: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 226
	i32 172961045, ; 24: Syncfusion.Maui.Core.dll => 0xa4f2d15 => 203
	i32 176265551, ; 25: System.ServiceProcess => 0xa81994f => 131
	i32 179457679, ; 26: Syncfusion.Maui.Expander => 0xab24e8f => 205
	i32 182336117, ; 27: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 272
	i32 184328833, ; 28: System.ValueTuple.dll => 0xafca281 => 150
	i32 195452805, ; 29: vi/Microsoft.Maui.Controls.resources.dll => 0xba65f85 => 324
	i32 199333315, ; 30: zh-HK/Microsoft.Maui.Controls.resources.dll => 0xbe195c3 => 325
	i32 205061960, ; 31: System.ComponentModel => 0xc38ff48 => 18
	i32 209399409, ; 32: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 224
	i32 220171995, ; 33: System.Diagnostics.Debug => 0xd1f8edb => 26
	i32 221958352, ; 34: Microsoft.Extensions.Diagnostics.dll => 0xd3ad0d0 => 182
	i32 230216969, ; 35: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 246
	i32 230752869, ; 36: Microsoft.CSharp.dll => 0xdc10265 => 1
	i32 231409092, ; 37: System.Linq.Parallel => 0xdcb05c4 => 58
	i32 231814094, ; 38: System.Globalization => 0xdd133ce => 41
	i32 246610117, ; 39: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 90
	i32 261689757, ; 40: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 229
	i32 276479776, ; 41: System.Threading.Timer.dll => 0x107abf20 => 146
	i32 278686392, ; 42: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 248
	i32 280482487, ; 43: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 245
	i32 280992041, ; 44: cs/Microsoft.Maui.Controls.resources.dll => 0x10bf9929 => 296
	i32 291076382, ; 45: System.IO.Pipes.AccessControl.dll => 0x1159791e => 53
	i32 291275502, ; 46: Microsoft.Extensions.Http.dll => 0x115c82ee => 184
	i32 298918909, ; 47: System.Net.Ping.dll => 0x11d123fd => 68
	i32 315399044, ; 48: Syncfusion.Maui.Expander.dll => 0x12cc9b84 => 205
	i32 317674968, ; 49: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 324
	i32 318968648, ; 50: Xamarin.AndroidX.Activity.dll => 0x13031348 => 215
	i32 321597661, ; 51: System.Numerics => 0x132b30dd => 82
	i32 336156722, ; 52: ja/Microsoft.Maui.Controls.resources.dll => 0x14095832 => 309
	i32 342366114, ; 53: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 247
	i32 356389973, ; 54: it/Microsoft.Maui.Controls.resources.dll => 0x153e1455 => 308
	i32 360082299, ; 55: System.ServiceModel.Web => 0x15766b7b => 130
	i32 367780167, ; 56: System.IO.Pipes => 0x15ebe147 => 54
	i32 374914964, ; 57: System.Transactions.Local => 0x1658bf94 => 148
	i32 375677976, ; 58: System.Net.ServicePoint.dll => 0x16646418 => 73
	i32 379916513, ; 59: System.Threading.Thread.dll => 0x16a510e1 => 144
	i32 382771021, ; 60: Microsoft.Bcl.Memory.dll => 0x16d09f4d => 176
	i32 385762202, ; 61: System.Memory.dll => 0x16fe439a => 61
	i32 392610295, ; 62: System.Threading.ThreadPool.dll => 0x1766c1f7 => 145
	i32 395744057, ; 63: _Microsoft.Android.Resource.Designer => 0x17969339 => 332
	i32 403441872, ; 64: WindowsBase => 0x180c08d0 => 164
	i32 435591531, ; 65: sv/Microsoft.Maui.Controls.resources.dll => 0x19f6996b => 320
	i32 441335492, ; 66: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 230
	i32 442565967, ; 67: System.Collections => 0x1a61054f => 12
	i32 450948140, ; 68: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 243
	i32 451504562, ; 69: System.Security.Cryptography.X509Certificates => 0x1ae969b2 => 124
	i32 456227837, ; 70: System.Web.HttpUtility.dll => 0x1b317bfd => 151
	i32 456681651, ; 71: Syncfusion.Maui.GridCommon.dll => 0x1b3868b3 => 206
	i32 459347974, ; 72: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 112
	i32 465846621, ; 73: mscorlib => 0x1bc4415d => 165
	i32 469710990, ; 74: System.dll => 0x1bff388e => 163
	i32 476646585, ; 75: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 245
	i32 485463106, ; 76: Microsoft.IdentityModel.Abstractions => 0x1cef9442 => 191
	i32 486930444, ; 77: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 258
	i32 498788369, ; 78: System.ObjectModel => 0x1dbae811 => 83
	i32 500358224, ; 79: id/Microsoft.Maui.Controls.resources.dll => 0x1dd2dc50 => 307
	i32 503918385, ; 80: fi/Microsoft.Maui.Controls.resources.dll => 0x1e092f31 => 301
	i32 513247710, ; 81: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 190
	i32 526420162, ; 82: System.Transactions.dll => 0x1f6088c2 => 149
	i32 527452488, ; 83: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 290
	i32 530272170, ; 84: System.Linq.Queryable => 0x1f9b4faa => 59
	i32 539058512, ; 85: Microsoft.Extensions.Logging => 0x20216150 => 185
	i32 540030774, ; 86: System.IO.FileSystem.dll => 0x20303736 => 50
	i32 545304856, ; 87: System.Runtime.Extensions => 0x2080b118 => 102
	i32 546455878, ; 88: System.Runtime.Serialization.Xml => 0x20924146 => 113
	i32 549171840, ; 89: System.Globalization.Calendars => 0x20bbb280 => 39
	i32 557405415, ; 90: Jsr305Binding => 0x213954e7 => 283
	i32 569601784, ; 91: Xamarin.AndroidX.Window.Extensions.Core.Core => 0x21f36ef8 => 281
	i32 577335427, ; 92: System.Security.Cryptography.Cng => 0x22697083 => 119
	i32 592146354, ; 93: pt-BR/Microsoft.Maui.Controls.resources.dll => 0x234b6fb2 => 315
	i32 597488923, ; 94: CommunityToolkit.Maui => 0x239cf51b => 172
	i32 601371474, ; 95: System.IO.IsolatedStorage.dll => 0x23d83352 => 51
	i32 605376203, ; 96: System.IO.Compression.FileSystem => 0x24154ecb => 43
	i32 613668793, ; 97: System.Security.Cryptography.Algorithms => 0x2493d7b9 => 118
	i32 627609679, ; 98: Xamarin.AndroidX.CustomView => 0x2568904f => 235
	i32 627931235, ; 99: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 313
	i32 639843206, ; 100: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x26233b86 => 241
	i32 643868501, ; 101: System.Net => 0x2660a755 => 80
	i32 662205335, ; 102: System.Text.Encodings.Web.dll => 0x27787397 => 135
	i32 663517072, ; 103: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 277
	i32 666292255, ; 104: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 222
	i32 672442732, ; 105: System.Collections.Concurrent => 0x2814a96c => 8
	i32 683518922, ; 106: System.Net.Security => 0x28bdabca => 72
	i32 688181140, ; 107: ca/Microsoft.Maui.Controls.resources.dll => 0x2904cf94 => 295
	i32 690569205, ; 108: System.Xml.Linq.dll => 0x29293ff5 => 154
	i32 691348768, ; 109: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 292
	i32 693804605, ; 110: System.Windows => 0x295a9e3d => 153
	i32 699345723, ; 111: System.Reflection.Emit => 0x29af2b3b => 91
	i32 700284507, ; 112: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 287
	i32 700358131, ; 113: System.IO.Compression.ZipFile => 0x29be9df3 => 44
	i32 706645707, ; 114: ko/Microsoft.Maui.Controls.resources.dll => 0x2a1e8ecb => 310
	i32 709557578, ; 115: de/Microsoft.Maui.Controls.resources.dll => 0x2a4afd4a => 298
	i32 720511267, ; 116: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 291
	i32 722857257, ; 117: System.Runtime.Loader.dll => 0x2b15ed29 => 108
	i32 731701662, ; 118: Microsoft.Extensions.Options.ConfigurationExtensions => 0x2b9ce19e => 189
	i32 735137430, ; 119: System.Security.SecureString.dll => 0x2bd14e96 => 128
	i32 752232764, ; 120: System.Diagnostics.Contracts.dll => 0x2cd6293c => 25
	i32 755313932, ; 121: Xamarin.Android.Glide.Annotations.dll => 0x2d052d0c => 212
	i32 759454413, ; 122: System.Net.Requests => 0x2d445acd => 71
	i32 762598435, ; 123: System.IO.Pipes.dll => 0x2d745423 => 54
	i32 775507847, ; 124: System.IO.Compression => 0x2e394f87 => 45
	i32 777317022, ; 125: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 319
	i32 789151979, ; 126: Microsoft.Extensions.Options => 0x2f0980eb => 188
	i32 790371945, ; 127: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 0x2f1c1e69 => 236
	i32 804715423, ; 128: System.Data.Common => 0x2ff6fb9f => 22
	i32 807930345, ; 129: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 0x302809e9 => 250
	i32 823281589, ; 130: System.Private.Uri.dll => 0x311247b5 => 85
	i32 830298997, ; 131: System.IO.Compression.Brotli => 0x317d5b75 => 42
	i32 832635846, ; 132: System.Xml.XPath.dll => 0x31a103c6 => 159
	i32 834051424, ; 133: System.Net.Quic => 0x31b69d60 => 70
	i32 843511501, ; 134: Xamarin.AndroidX.Print => 0x3246f6cd => 263
	i32 873119928, ; 135: Microsoft.VisualBasic => 0x340ac0b8 => 3
	i32 877678880, ; 136: System.Globalization.dll => 0x34505120 => 41
	i32 878954865, ; 137: System.Net.Http.Json => 0x3463c971 => 62
	i32 904024072, ; 138: System.ComponentModel.Primitives.dll => 0x35e25008 => 16
	i32 911108515, ; 139: System.IO.MemoryMappedFiles.dll => 0x364e69a3 => 52
	i32 926902833, ; 140: tr/Microsoft.Maui.Controls.resources.dll => 0x373f6a31 => 322
	i32 928116545, ; 141: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 286
	i32 952186615, ; 142: System.Runtime.InteropServices.JavaScript.dll => 0x38c136f7 => 104
	i32 955402788, ; 143: Newtonsoft.Json => 0x38f24a24 => 201
	i32 956575887, ; 144: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 291
	i32 966729478, ; 145: Xamarin.Google.Crypto.Tink.Android => 0x399f1f06 => 284
	i32 967690846, ; 146: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 247
	i32 975236339, ; 147: System.Diagnostics.Tracing => 0x3a20ecf3 => 33
	i32 975874589, ; 148: System.Xml.XDocument => 0x3a2aaa1d => 157
	i32 986514023, ; 149: System.Private.DataContractSerialization.dll => 0x3acd0267 => 84
	i32 987214855, ; 150: System.Diagnostics.Tools => 0x3ad7b407 => 31
	i32 992768348, ; 151: System.Collections.dll => 0x3b2c715c => 12
	i32 994442037, ; 152: System.IO.FileSystem => 0x3b45fb35 => 50
	i32 1001831731, ; 153: System.IO.UnmanagedMemoryStream.dll => 0x3bb6bd33 => 55
	i32 1012816738, ; 154: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 267
	i32 1019214401, ; 155: System.Drawing => 0x3cbffa41 => 35
	i32 1028951442, ; 156: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 181
	i32 1029334545, ; 157: da/Microsoft.Maui.Controls.resources.dll => 0x3d5a6611 => 297
	i32 1031528504, ; 158: Xamarin.Google.ErrorProne.Annotations.dll => 0x3d7be038 => 285
	i32 1035644815, ; 159: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 220
	i32 1036536393, ; 160: System.Drawing.Primitives.dll => 0x3dc84a49 => 34
	i32 1044663988, ; 161: System.Linq.Expressions.dll => 0x3e444eb4 => 57
	i32 1048992957, ; 162: Microsoft.Extensions.Diagnostics.Abstractions.dll => 0x3e865cbd => 183
	i32 1052210849, ; 163: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 254
	i32 1067306892, ; 164: GoogleGson => 0x3f9dcf8c => 175
	i32 1082857460, ; 165: System.ComponentModel.TypeConverter => 0x408b17f4 => 17
	i32 1084122840, ; 166: Xamarin.Kotlin.StdLib => 0x409e66d8 => 288
	i32 1098259244, ; 167: System => 0x41761b2c => 163
	i32 1118262833, ; 168: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 310
	i32 1121599056, ; 169: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 0x42da3e50 => 253
	i32 1127624469, ; 170: Microsoft.Extensions.Logging.Debug => 0x43362f15 => 187
	i32 1149092582, ; 171: Xamarin.AndroidX.Window => 0x447dc2e6 => 280
	i32 1162065116, ; 172: Microsoft.Bcl.Memory => 0x4543b4dc => 176
	i32 1168523401, ; 173: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 316
	i32 1170634674, ; 174: System.Web.dll => 0x45c677b2 => 152
	i32 1175144683, ; 175: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 276
	i32 1178241025, ; 176: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 261
	i32 1203215381, ; 177: pl/Microsoft.Maui.Controls.resources.dll => 0x47b79c15 => 314
	i32 1204270330, ; 178: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 222
	i32 1208641965, ; 179: System.Diagnostics.Process => 0x480a69ad => 28
	i32 1214827643, ; 180: CommunityToolkit.Mvvm => 0x4868cc7b => 174
	i32 1219128291, ; 181: System.IO.IsolatedStorage => 0x48aa6be3 => 51
	i32 1234928153, ; 182: nb/Microsoft.Maui.Controls.resources.dll => 0x499b8219 => 312
	i32 1243150071, ; 183: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 0x4a18f6f7 => 281
	i32 1253011324, ; 184: Microsoft.Win32.Registry => 0x4aaf6f7c => 5
	i32 1260983243, ; 185: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 296
	i32 1264511973, ; 186: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 271
	i32 1267360935, ; 187: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 275
	i32 1273260888, ; 188: Xamarin.AndroidX.Collection.Ktx => 0x4be46b58 => 227
	i32 1275534314, ; 189: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 292
	i32 1278448581, ; 190: Xamarin.AndroidX.Annotation.Jvm => 0x4c3393c5 => 219
	i32 1293217323, ; 191: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 238
	i32 1309188875, ; 192: System.Private.DataContractSerialization => 0x4e08a30b => 84
	i32 1322716291, ; 193: Xamarin.AndroidX.Window.dll => 0x4ed70c83 => 280
	i32 1324164729, ; 194: System.Linq => 0x4eed2679 => 60
	i32 1335329327, ; 195: System.Runtime.Serialization.Json.dll => 0x4f97822f => 111
	i32 1364015309, ; 196: System.IO => 0x514d38cd => 56
	i32 1373134921, ; 197: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 326
	i32 1376866003, ; 198: Xamarin.AndroidX.SavedState => 0x52114ed3 => 267
	i32 1379779777, ; 199: System.Resources.ResourceManager => 0x523dc4c1 => 98
	i32 1402170036, ; 200: System.Configuration.dll => 0x53936ab4 => 19
	i32 1406073936, ; 201: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 231
	i32 1408764838, ; 202: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 110
	i32 1411638395, ; 203: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 100
	i32 1422545099, ; 204: System.Runtime.CompilerServices.VisualC => 0x54ca50cb => 101
	i32 1430672901, ; 205: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 294
	i32 1434145427, ; 206: System.Runtime.Handles => 0x557b5293 => 103
	i32 1435222561, ; 207: Xamarin.Google.Crypto.Tink.Android.dll => 0x558bc221 => 284
	i32 1439761251, ; 208: System.Net.Quic.dll => 0x55d10363 => 70
	i32 1452070440, ; 209: System.Formats.Asn1.dll => 0x568cd628 => 37
	i32 1453312822, ; 210: System.Diagnostics.Tools.dll => 0x569fcb36 => 31
	i32 1457743152, ; 211: System.Runtime.Extensions.dll => 0x56e36530 => 102
	i32 1458022317, ; 212: System.Net.Security.dll => 0x56e7a7ad => 72
	i32 1460893475, ; 213: System.IdentityModel.Tokens.Jwt => 0x57137723 => 210
	i32 1461004990, ; 214: es\Microsoft.Maui.Controls.resources => 0x57152abe => 300
	i32 1461234159, ; 215: System.Collections.Immutable.dll => 0x5718a9ef => 9
	i32 1461719063, ; 216: System.Security.Cryptography.OpenSsl => 0x57201017 => 122
	i32 1462112819, ; 217: System.IO.Compression.dll => 0x57261233 => 45
	i32 1469204771, ; 218: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 221
	i32 1470490898, ; 219: Microsoft.Extensions.Primitives => 0x57a5e912 => 190
	i32 1479771757, ; 220: System.Collections.Immutable => 0x5833866d => 9
	i32 1480492111, ; 221: System.IO.Compression.Brotli.dll => 0x583e844f => 42
	i32 1487239319, ; 222: Microsoft.Win32.Primitives => 0x58a57897 => 4
	i32 1490025113, ; 223: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 0x58cffa99 => 268
	i32 1493001747, ; 224: hi/Microsoft.Maui.Controls.resources.dll => 0x58fd6613 => 304
	i32 1498168481, ; 225: Microsoft.IdentityModel.JsonWebTokens.dll => 0x594c3ca1 => 192
	i32 1505131794, ; 226: Microsoft.Extensions.Http => 0x59b67d12 => 184
	i32 1514721132, ; 227: el/Microsoft.Maui.Controls.resources.dll => 0x5a48cf6c => 299
	i32 1536373174, ; 228: System.Diagnostics.TextWriterTraceListener => 0x5b9331b6 => 30
	i32 1543031311, ; 229: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 137
	i32 1543355203, ; 230: System.Reflection.Emit.dll => 0x5bfdbb43 => 91
	i32 1550322496, ; 231: System.Reflection.Extensions.dll => 0x5c680b40 => 92
	i32 1551623176, ; 232: sk/Microsoft.Maui.Controls.resources.dll => 0x5c7be408 => 319
	i32 1565862583, ; 233: System.IO.FileSystem.Primitives => 0x5d552ab7 => 48
	i32 1566207040, ; 234: System.Threading.Tasks.Dataflow.dll => 0x5d5a6c40 => 140
	i32 1573704789, ; 235: System.Runtime.Serialization.Json => 0x5dccd455 => 111
	i32 1580037396, ; 236: System.Threading.Overlapped => 0x5e2d7514 => 139
	i32 1582372066, ; 237: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 237
	i32 1592978981, ; 238: System.Runtime.Serialization.dll => 0x5ef2ee25 => 114
	i32 1597949149, ; 239: Xamarin.Google.ErrorProne.Annotations => 0x5f3ec4dd => 285
	i32 1601112923, ; 240: System.Xml.Serialization => 0x5f6f0b5b => 156
	i32 1603525486, ; 241: Microsoft.Maui.Controls.HotReload.Forms.dll => 0x5f93db6e => 329
	i32 1604827217, ; 242: System.Net.WebClient => 0x5fa7b851 => 75
	i32 1618516317, ; 243: System.Net.WebSockets.Client.dll => 0x6078995d => 78
	i32 1622152042, ; 244: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 257
	i32 1622358360, ; 245: System.Dynamic.Runtime => 0x60b33958 => 36
	i32 1624863272, ; 246: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 279
	i32 1634654947, ; 247: CommunityToolkit.Maui.Core.dll => 0x616edae3 => 173
	i32 1635184631, ; 248: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x6176eff7 => 241
	i32 1636350590, ; 249: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 234
	i32 1639515021, ; 250: System.Net.Http.dll => 0x61b9038d => 63
	i32 1639986890, ; 251: System.Text.RegularExpressions => 0x61c036ca => 137
	i32 1641389582, ; 252: System.ComponentModel.EventBasedAsync.dll => 0x61d59e0e => 15
	i32 1653151792, ; 253: Syncfusion.Maui.Inputs.dll => 0x62891830 => 207
	i32 1657153582, ; 254: System.Runtime => 0x62c6282e => 115
	i32 1658241508, ; 255: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 273
	i32 1658251792, ; 256: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 282
	i32 1670060433, ; 257: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 229
	i32 1675553242, ; 258: System.IO.FileSystem.DriveInfo.dll => 0x63dee9da => 47
	i32 1677501392, ; 259: System.Net.Primitives.dll => 0x63fca3d0 => 69
	i32 1678508291, ; 260: System.Net.WebSockets => 0x640c0103 => 79
	i32 1679769178, ; 261: System.Security.Cryptography => 0x641f3e5a => 125
	i32 1691477237, ; 262: System.Reflection.Metadata => 0x64d1e4f5 => 93
	i32 1696967625, ; 263: System.Security.Cryptography.Csp => 0x6525abc9 => 120
	i32 1698840827, ; 264: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 289
	i32 1701541528, ; 265: System.Diagnostics.Debug.dll => 0x656b7698 => 26
	i32 1720223769, ; 266: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 0x66888819 => 250
	i32 1726116996, ; 267: System.Reflection.dll => 0x66e27484 => 96
	i32 1728033016, ; 268: System.Diagnostics.FileVersionInfo.dll => 0x66ffb0f8 => 27
	i32 1729485958, ; 269: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 225
	i32 1736233607, ; 270: ro/Microsoft.Maui.Controls.resources.dll => 0x677cd287 => 317
	i32 1743415430, ; 271: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 295
	i32 1744735666, ; 272: System.Transactions.Local.dll => 0x67fe8db2 => 148
	i32 1746316138, ; 273: Mono.Android.Export => 0x6816ab6a => 168
	i32 1750313021, ; 274: Microsoft.Win32.Primitives.dll => 0x6853a83d => 4
	i32 1758240030, ; 275: System.Resources.Reader.dll => 0x68cc9d1e => 97
	i32 1763938596, ; 276: System.Diagnostics.TraceSource.dll => 0x69239124 => 32
	i32 1765942094, ; 277: System.Reflection.Extensions => 0x6942234e => 92
	i32 1766324549, ; 278: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 272
	i32 1770582343, ; 279: Microsoft.Extensions.Logging.dll => 0x6988f147 => 185
	i32 1776026572, ; 280: System.Core.dll => 0x69dc03cc => 21
	i32 1777075843, ; 281: System.Globalization.Extensions.dll => 0x69ec0683 => 40
	i32 1780572499, ; 282: Mono.Android.Runtime.dll => 0x6a216153 => 169
	i32 1782862114, ; 283: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 311
	i32 1788241197, ; 284: Xamarin.AndroidX.Fragment => 0x6a96652d => 243
	i32 1793755602, ; 285: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 303
	i32 1808609942, ; 286: Xamarin.AndroidX.Loader => 0x6bcd3296 => 257
	i32 1813058853, ; 287: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 288
	i32 1813201214, ; 288: Xamarin.Google.Android.Material => 0x6c13413e => 282
	i32 1818569960, ; 289: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 262
	i32 1818787751, ; 290: Microsoft.VisualBasic.Core => 0x6c687fa7 => 2
	i32 1824175904, ; 291: System.Text.Encoding.Extensions => 0x6cbab720 => 133
	i32 1824722060, ; 292: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 110
	i32 1827303595, ; 293: Microsoft.VisualStudio.DesignTools.TapContract => 0x6cea70ab => 331
	i32 1828327463, ; 294: DigesettAPP => 0x6cfa1027 => 0
	i32 1828688058, ; 295: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 186
	i32 1842015223, ; 296: uk/Microsoft.Maui.Controls.resources.dll => 0x6dcaebf7 => 323
	i32 1847515442, ; 297: Xamarin.Android.Glide.Annotations => 0x6e1ed932 => 212
	i32 1853025655, ; 298: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 320
	i32 1858542181, ; 299: System.Linq.Expressions => 0x6ec71a65 => 57
	i32 1870277092, ; 300: System.Reflection.Primitives => 0x6f7a29e4 => 94
	i32 1875935024, ; 301: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 302
	i32 1879696579, ; 302: System.Formats.Tar.dll => 0x7009e4c3 => 38
	i32 1885316902, ; 303: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 223
	i32 1885918049, ; 304: Microsoft.VisualStudio.DesignTools.TapContract.dll => 0x7068d361 => 331
	i32 1888955245, ; 305: System.Diagnostics.Contracts => 0x70972b6d => 25
	i32 1889954781, ; 306: System.Reflection.Metadata.dll => 0x70a66bdd => 93
	i32 1898237753, ; 307: System.Reflection.DispatchProxy => 0x7124cf39 => 88
	i32 1900610850, ; 308: System.Resources.ResourceManager.dll => 0x71490522 => 98
	i32 1910275211, ; 309: System.Collections.NonGeneric.dll => 0x71dc7c8b => 10
	i32 1939592360, ; 310: System.Private.Xml.Linq => 0x739bd4a8 => 86
	i32 1956758971, ; 311: System.Resources.Writer => 0x74a1c5bb => 99
	i32 1961813231, ; 312: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x74eee4ef => 269
	i32 1968388702, ; 313: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 177
	i32 1983156543, ; 314: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 289
	i32 1985761444, ; 315: Xamarin.Android.Glide.GifDecoder => 0x765c50a4 => 214
	i32 1986222447, ; 316: Microsoft.IdentityModel.Tokens.dll => 0x7663596f => 194
	i32 2003115576, ; 317: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 299
	i32 2011961780, ; 318: System.Buffers.dll => 0x77ec19b4 => 7
	i32 2019465201, ; 319: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 254
	i32 2025202353, ; 320: ar/Microsoft.Maui.Controls.resources.dll => 0x78b622b1 => 294
	i32 2031763787, ; 321: Xamarin.Android.Glide => 0x791a414b => 211
	i32 2045470958, ; 322: System.Private.Xml => 0x79eb68ee => 87
	i32 2048278909, ; 323: Microsoft.Extensions.Configuration.Binder.dll => 0x7a16417d => 179
	i32 2055257422, ; 324: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 249
	i32 2060060697, ; 325: System.Windows.dll => 0x7aca0819 => 153
	i32 2066184531, ; 326: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 298
	i32 2070888862, ; 327: System.Diagnostics.TraceSource => 0x7b6f419e => 32
	i32 2079903147, ; 328: System.Runtime.dll => 0x7bf8cdab => 115
	i32 2090596640, ; 329: System.Numerics.Vectors => 0x7c9bf920 => 81
	i32 2127167465, ; 330: System.Console => 0x7ec9ffe9 => 20
	i32 2142473426, ; 331: System.Collections.Specialized => 0x7fb38cd2 => 11
	i32 2143790110, ; 332: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 161
	i32 2146852085, ; 333: Microsoft.VisualBasic.dll => 0x7ff65cf5 => 3
	i32 2159891885, ; 334: Microsoft.Maui => 0x80bd55ad => 198
	i32 2169148018, ; 335: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 306
	i32 2181898931, ; 336: Microsoft.Extensions.Options.dll => 0x820d22b3 => 188
	i32 2192057212, ; 337: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 186
	i32 2193016926, ; 338: System.ObjectModel.dll => 0x82b6c85e => 83
	i32 2201107256, ; 339: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 293
	i32 2201231467, ; 340: System.Net.Http => 0x8334206b => 63
	i32 2207618523, ; 341: it\Microsoft.Maui.Controls.resources => 0x839595db => 308
	i32 2217644978, ; 342: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 276
	i32 2222056684, ; 343: System.Threading.Tasks.Parallel => 0x8471e4ec => 142
	i32 2244775296, ; 344: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 258
	i32 2252106437, ; 345: System.Xml.Serialization.dll => 0x863c6ac5 => 156
	i32 2256313426, ; 346: System.Globalization.Extensions => 0x867c9c52 => 40
	i32 2265110946, ; 347: System.Security.AccessControl.dll => 0x8702d9a2 => 116
	i32 2266799131, ; 348: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 178
	i32 2267999099, ; 349: Xamarin.Android.Glide.DiskLruCache.dll => 0x872eeb7b => 213
	i32 2270573516, ; 350: fr/Microsoft.Maui.Controls.resources.dll => 0x875633cc => 302
	i32 2279755925, ; 351: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 265
	i32 2293034957, ; 352: System.ServiceModel.Web.dll => 0x88acefcd => 130
	i32 2295906218, ; 353: System.Net.Sockets => 0x88d8bfaa => 74
	i32 2298471582, ; 354: System.Net.Mail => 0x88ffe49e => 65
	i32 2303942373, ; 355: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 312
	i32 2305521784, ; 356: System.Private.CoreLib.dll => 0x896b7878 => 171
	i32 2315684594, ; 357: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 217
	i32 2320631194, ; 358: System.Threading.Tasks.Parallel.dll => 0x8a52059a => 142
	i32 2340441535, ; 359: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 105
	i32 2344264397, ; 360: System.ValueTuple => 0x8bbaa2cd => 150
	i32 2353062107, ; 361: System.Net.Primitives => 0x8c40e0db => 69
	i32 2354730003, ; 362: Syncfusion.Licensing => 0x8c5a5413 => 202
	i32 2368005991, ; 363: System.Xml.ReaderWriter.dll => 0x8d24e767 => 155
	i32 2369706906, ; 364: Microsoft.IdentityModel.Logging => 0x8d3edb9a => 193
	i32 2371007202, ; 365: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 177
	i32 2378619854, ; 366: System.Security.Cryptography.Csp.dll => 0x8dc6dbce => 120
	i32 2383496789, ; 367: System.Security.Principal.Windows.dll => 0x8e114655 => 126
	i32 2395872292, ; 368: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 307
	i32 2401565422, ; 369: System.Web.HttpUtility => 0x8f24faee => 151
	i32 2403452196, ; 370: Xamarin.AndroidX.Emoji2.dll => 0x8f41c524 => 240
	i32 2409983638, ; 371: Microsoft.VisualStudio.DesignTools.MobileTapContracts.dll => 0x8fa56e96 => 330
	i32 2421380589, ; 372: System.Threading.Tasks.Dataflow => 0x905355ed => 140
	i32 2423080555, ; 373: Xamarin.AndroidX.Collection.Ktx.dll => 0x906d466b => 227
	i32 2426750280, ; 374: en-US\Syncfusion.Maui.Inputs.resources => 0x90a54548 => 328
	i32 2427813419, ; 375: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 304
	i32 2435356389, ; 376: System.Console.dll => 0x912896e5 => 20
	i32 2435904999, ; 377: System.ComponentModel.DataAnnotations.dll => 0x9130f5e7 => 14
	i32 2454642406, ; 378: System.Text.Encoding.dll => 0x924edee6 => 134
	i32 2458678730, ; 379: System.Net.Sockets.dll => 0x928c75ca => 74
	i32 2459001652, ; 380: System.Linq.Parallel.dll => 0x92916334 => 58
	i32 2465532216, ; 381: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 230
	i32 2471841756, ; 382: netstandard.dll => 0x93554fdc => 166
	i32 2475788418, ; 383: Java.Interop.dll => 0x93918882 => 167
	i32 2480646305, ; 384: Microsoft.Maui.Controls => 0x93dba8a1 => 196
	i32 2483903535, ; 385: System.ComponentModel.EventBasedAsync => 0x940d5c2f => 15
	i32 2484371297, ; 386: System.Net.ServicePoint => 0x94147f61 => 73
	i32 2490993605, ; 387: System.AppContext.dll => 0x94798bc5 => 6
	i32 2501346920, ; 388: System.Data.DataSetExtensions => 0x95178668 => 23
	i32 2505896520, ; 389: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 252
	i32 2522472828, ; 390: Xamarin.Android.Glide.dll => 0x9659e17c => 211
	i32 2538310050, ; 391: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 90
	i32 2550873716, ; 392: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 305
	i32 2562349572, ; 393: Microsoft.CSharp => 0x98ba5a04 => 1
	i32 2570120770, ; 394: System.Text.Encodings.Web => 0x9930ee42 => 135
	i32 2581783588, ; 395: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 0x99e2e424 => 253
	i32 2581819634, ; 396: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 275
	i32 2585220780, ; 397: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 133
	i32 2585805581, ; 398: System.Net.Ping => 0x9a20430d => 68
	i32 2589602615, ; 399: System.Threading.ThreadPool => 0x9a5a3337 => 145
	i32 2593496499, ; 400: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 314
	i32 2605712449, ; 401: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 293
	i32 2615233544, ; 402: Xamarin.AndroidX.Fragment.Ktx => 0x9be14c08 => 244
	i32 2616218305, ; 403: Microsoft.Extensions.Logging.Debug.dll => 0x9bf052c1 => 187
	i32 2617129537, ; 404: System.Private.Xml.dll => 0x9bfe3a41 => 87
	i32 2618712057, ; 405: System.Reflection.TypeExtensions.dll => 0x9c165ff9 => 95
	i32 2620871830, ; 406: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 234
	i32 2624644809, ; 407: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 239
	i32 2626831493, ; 408: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 309
	i32 2627185994, ; 409: System.Diagnostics.TextWriterTraceListener.dll => 0x9c97ad4a => 30
	i32 2629843544, ; 410: System.IO.Compression.ZipFile.dll => 0x9cc03a58 => 44
	i32 2633051222, ; 411: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 248
	i32 2640290731, ; 412: Microsoft.IdentityModel.Logging.dll => 0x9d5fa3ab => 193
	i32 2663391936, ; 413: Xamarin.Android.Glide.DiskLruCache => 0x9ec022c0 => 213
	i32 2663698177, ; 414: System.Runtime.Loader => 0x9ec4cf01 => 108
	i32 2664396074, ; 415: System.Xml.XDocument.dll => 0x9ecf752a => 157
	i32 2665622720, ; 416: System.Drawing.Primitives => 0x9ee22cc0 => 34
	i32 2676780864, ; 417: System.Data.Common.dll => 0x9f8c6f40 => 22
	i32 2686887180, ; 418: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 113
	i32 2693849962, ; 419: System.IO.dll => 0xa090e36a => 56
	i32 2701096212, ; 420: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 273
	i32 2715334215, ; 421: System.Threading.Tasks.dll => 0xa1d8b647 => 143
	i32 2717744543, ; 422: System.Security.Claims => 0xa1fd7d9f => 117
	i32 2719963679, ; 423: System.Security.Cryptography.Cng.dll => 0xa21f5a1f => 119
	i32 2724373263, ; 424: System.Runtime.Numerics.dll => 0xa262a30f => 109
	i32 2732626843, ; 425: Xamarin.AndroidX.Activity => 0xa2e0939b => 215
	i32 2735172069, ; 426: System.Threading.Channels => 0xa30769e5 => 138
	i32 2737747696, ; 427: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 221
	i32 2740948882, ; 428: System.IO.Pipes.AccessControl => 0xa35f8f92 => 53
	i32 2743368605, ; 429: Syncfusion.Maui.DataSource => 0xa3847b9d => 204
	i32 2748088231, ; 430: System.Runtime.InteropServices.JavaScript => 0xa3cc7fa7 => 104
	i32 2752995522, ; 431: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 315
	i32 2758225723, ; 432: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 197
	i32 2764765095, ; 433: Microsoft.Maui.dll => 0xa4caf7a7 => 198
	i32 2765824710, ; 434: System.Text.Encoding.CodePages.dll => 0xa4db22c6 => 132
	i32 2770495804, ; 435: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 287
	i32 2778768386, ; 436: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 278
	i32 2779977773, ; 437: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0xa5b3182d => 266
	i32 2785988530, ; 438: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 321
	i32 2788224221, ; 439: Xamarin.AndroidX.Fragment.Ktx.dll => 0xa630ecdd => 244
	i32 2801831435, ; 440: Microsoft.Maui.Graphics => 0xa7008e0b => 200
	i32 2803228030, ; 441: System.Xml.XPath.XDocument.dll => 0xa715dd7e => 158
	i32 2806116107, ; 442: es/Microsoft.Maui.Controls.resources.dll => 0xa741ef0b => 300
	i32 2810250172, ; 443: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 231
	i32 2819470561, ; 444: System.Xml.dll => 0xa80db4e1 => 162
	i32 2821205001, ; 445: System.ServiceProcess.dll => 0xa8282c09 => 131
	i32 2821294376, ; 446: Xamarin.AndroidX.ResourceInspection.Annotation => 0xa8298928 => 266
	i32 2824502124, ; 447: System.Xml.XmlDocument => 0xa85a7b6c => 160
	i32 2831556043, ; 448: nl/Microsoft.Maui.Controls.resources.dll => 0xa8c61dcb => 313
	i32 2838993487, ; 449: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 0xa9379a4f => 255
	i32 2849599387, ; 450: System.Threading.Overlapped.dll => 0xa9d96f9b => 139
	i32 2853208004, ; 451: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 278
	i32 2855708567, ; 452: Xamarin.AndroidX.Transition => 0xaa36a797 => 274
	i32 2861098320, ; 453: Mono.Android.Export.dll => 0xaa88e550 => 168
	i32 2861189240, ; 454: Microsoft.Maui.Essentials => 0xaa8a4878 => 199
	i32 2868488919, ; 455: CommunityToolkit.Maui.Core => 0xaaf9aad7 => 173
	i32 2868557005, ; 456: Syncfusion.Licensing.dll => 0xaafab4cd => 202
	i32 2870099610, ; 457: Xamarin.AndroidX.Activity.Ktx.dll => 0xab123e9a => 216
	i32 2875164099, ; 458: Jsr305Binding.dll => 0xab5f85c3 => 283
	i32 2875220617, ; 459: System.Globalization.Calendars.dll => 0xab606289 => 39
	i32 2884993177, ; 460: Xamarin.AndroidX.ExifInterface => 0xabf58099 => 242
	i32 2887636118, ; 461: System.Net.dll => 0xac1dd496 => 80
	i32 2899753641, ; 462: System.IO.UnmanagedMemoryStream => 0xacd6baa9 => 55
	i32 2900621748, ; 463: System.Dynamic.Runtime.dll => 0xace3f9b4 => 36
	i32 2901442782, ; 464: System.Reflection => 0xacf080de => 96
	i32 2905242038, ; 465: mscorlib.dll => 0xad2a79b6 => 165
	i32 2909740682, ; 466: System.Private.CoreLib => 0xad6f1e8a => 171
	i32 2916838712, ; 467: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 279
	i32 2919462931, ; 468: System.Numerics.Vectors.dll => 0xae037813 => 81
	i32 2921128767, ; 469: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 218
	i32 2936416060, ; 470: System.Resources.Reader => 0xaf06273c => 97
	i32 2940926066, ; 471: System.Diagnostics.StackTrace.dll => 0xaf4af872 => 29
	i32 2942453041, ; 472: System.Xml.XPath.XDocument => 0xaf624531 => 158
	i32 2959614098, ; 473: System.ComponentModel.dll => 0xb0682092 => 18
	i32 2968338931, ; 474: System.Security.Principal.Windows => 0xb0ed41f3 => 126
	i32 2971004615, ; 475: Microsoft.Extensions.Options.ConfigurationExtensions.dll => 0xb115eec7 => 189
	i32 2972252294, ; 476: System.Security.Cryptography.Algorithms.dll => 0xb128f886 => 118
	i32 2978675010, ; 477: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 238
	i32 2987532451, ; 478: Xamarin.AndroidX.Security.SecurityCrypto => 0xb21220a3 => 269
	i32 2996846495, ; 479: Xamarin.AndroidX.Lifecycle.Process.dll => 0xb2a03f9f => 251
	i32 3016983068, ; 480: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 271
	i32 3020703001, ; 481: Microsoft.Extensions.Diagnostics => 0xb40c4519 => 182
	i32 3023353419, ; 482: WindowsBase.dll => 0xb434b64b => 164
	i32 3024354802, ; 483: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 246
	i32 3038032645, ; 484: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 332
	i32 3056245963, ; 485: Xamarin.AndroidX.SavedState.SavedState.Ktx => 0xb62a9ccb => 268
	i32 3057625584, ; 486: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 259
	i32 3059408633, ; 487: Mono.Android.Runtime => 0xb65adef9 => 169
	i32 3059793426, ; 488: System.ComponentModel.Primitives => 0xb660be12 => 16
	i32 3075834255, ; 489: System.Threading.Tasks => 0xb755818f => 143
	i32 3077302341, ; 490: hu/Microsoft.Maui.Controls.resources.dll => 0xb76be845 => 306
	i32 3084678329, ; 491: Microsoft.IdentityModel.Tokens => 0xb7dc74b9 => 194
	i32 3090735792, ; 492: System.Security.Cryptography.X509Certificates.dll => 0xb838e2b0 => 124
	i32 3099732863, ; 493: System.Security.Claims.dll => 0xb8c22b7f => 117
	i32 3103600923, ; 494: System.Formats.Asn1 => 0xb8fd311b => 37
	i32 3111772706, ; 495: System.Runtime.Serialization => 0xb979e222 => 114
	i32 3121463068, ; 496: System.IO.FileSystem.AccessControl.dll => 0xba0dbf1c => 46
	i32 3124832203, ; 497: System.Threading.Tasks.Extensions => 0xba4127cb => 141
	i32 3132293585, ; 498: System.Security.AccessControl => 0xbab301d1 => 116
	i32 3140633799, ; 499: Syncfusion.Maui.ListView => 0xbb3244c7 => 208
	i32 3147165239, ; 500: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 33
	i32 3147228406, ; 501: Syncfusion.Maui.Core => 0xbb96e4f6 => 203
	i32 3148237826, ; 502: GoogleGson.dll => 0xbba64c02 => 175
	i32 3159123045, ; 503: System.Reflection.Primitives.dll => 0xbc4c6465 => 94
	i32 3160747431, ; 504: System.IO.MemoryMappedFiles => 0xbc652da7 => 52
	i32 3170039328, ; 505: Syncfusion.Maui.ListView.dll => 0xbcf2f620 => 208
	i32 3178803400, ; 506: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 260
	i32 3192346100, ; 507: System.Security.SecureString => 0xbe4755f4 => 128
	i32 3193515020, ; 508: System.Web => 0xbe592c0c => 152
	i32 3204380047, ; 509: System.Data.dll => 0xbefef58f => 24
	i32 3209718065, ; 510: System.Xml.XmlDocument.dll => 0xbf506931 => 160
	i32 3211777861, ; 511: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 237
	i32 3220365878, ; 512: System.Threading => 0xbff2e236 => 147
	i32 3226221578, ; 513: System.Runtime.Handles.dll => 0xc04c3c0a => 103
	i32 3251039220, ; 514: System.Reflection.DispatchProxy.dll => 0xc1c6ebf4 => 88
	i32 3258312781, ; 515: Xamarin.AndroidX.CardView => 0xc235e84d => 225
	i32 3265493905, ; 516: System.Linq.Queryable.dll => 0xc2a37b91 => 59
	i32 3265893370, ; 517: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 141
	i32 3277815716, ; 518: System.Resources.Writer.dll => 0xc35f7fa4 => 99
	i32 3279906254, ; 519: Microsoft.Win32.Registry.dll => 0xc37f65ce => 5
	i32 3280506390, ; 520: System.ComponentModel.Annotations.dll => 0xc3888e16 => 13
	i32 3290767353, ; 521: System.Security.Cryptography.Encoding => 0xc4251ff9 => 121
	i32 3299363146, ; 522: System.Text.Encoding => 0xc4a8494a => 134
	i32 3303498502, ; 523: System.Diagnostics.FileVersionInfo => 0xc4e76306 => 27
	i32 3305363605, ; 524: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 301
	i32 3306970809, ; 525: Syncfusion.Maui.Inputs => 0xc51c5eb9 => 207
	i32 3312457198, ; 526: Microsoft.IdentityModel.JsonWebTokens => 0xc57015ee => 192
	i32 3316684772, ; 527: System.Net.Requests.dll => 0xc5b097e4 => 71
	i32 3317135071, ; 528: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 235
	i32 3317144872, ; 529: System.Data => 0xc5b79d28 => 24
	i32 3340431453, ; 530: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 223
	i32 3345895724, ; 531: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 264
	i32 3346324047, ; 532: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 261
	i32 3357674450, ; 533: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 318
	i32 3358260929, ; 534: System.Text.Json => 0xc82afec1 => 136
	i32 3362336904, ; 535: Xamarin.AndroidX.Activity.Ktx => 0xc8693088 => 216
	i32 3362522851, ; 536: Xamarin.AndroidX.Core => 0xc86c06e3 => 232
	i32 3366347497, ; 537: Java.Interop => 0xc8a662e9 => 167
	i32 3374999561, ; 538: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 265
	i32 3381016424, ; 539: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 297
	i32 3381934622, ; 540: Syncfusion.Maui.GridCommon => 0xc9943a1e => 206
	i32 3395150330, ; 541: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 100
	i32 3403906625, ; 542: System.Security.Cryptography.OpenSsl.dll => 0xcae37e41 => 122
	i32 3405233483, ; 543: Xamarin.AndroidX.CustomView.PoolingContainer => 0xcaf7bd4b => 236
	i32 3421170118, ; 544: Microsoft.Extensions.Configuration.Binder => 0xcbeae9c6 => 179
	i32 3428513518, ; 545: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 180
	i32 3429136800, ; 546: System.Xml => 0xcc6479a0 => 162
	i32 3430777524, ; 547: netstandard => 0xcc7d82b4 => 166
	i32 3441283291, ; 548: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 239
	i32 3445260447, ; 549: System.Formats.Tar => 0xcd5a809f => 38
	i32 3452344032, ; 550: Microsoft.Maui.Controls.Compatibility.dll => 0xcdc696e0 => 195
	i32 3463511458, ; 551: hr/Microsoft.Maui.Controls.resources.dll => 0xce70fda2 => 305
	i32 3471940407, ; 552: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 17
	i32 3476120550, ; 553: Mono.Android => 0xcf3163e6 => 170
	i32 3479583265, ; 554: ru/Microsoft.Maui.Controls.resources.dll => 0xcf663a21 => 318
	i32 3484440000, ; 555: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 317
	i32 3485117614, ; 556: System.Text.Json.dll => 0xcfbaacae => 136
	i32 3486566296, ; 557: System.Transactions => 0xcfd0c798 => 149
	i32 3493954962, ; 558: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 228
	i32 3509114376, ; 559: System.Xml.Linq => 0xd128d608 => 154
	i32 3515174580, ; 560: System.Security.dll => 0xd1854eb4 => 129
	i32 3530912306, ; 561: System.Configuration => 0xd2757232 => 19
	i32 3539954161, ; 562: System.Net.HttpListener => 0xd2ff69f1 => 64
	i32 3558305335, ; 563: Syncfusion.Maui.DataSource.dll => 0xd4176e37 => 204
	i32 3560100363, ; 564: System.Threading.Timer => 0xd432d20b => 146
	i32 3570554715, ; 565: System.IO.FileSystem.AccessControl => 0xd4d2575b => 46
	i32 3580758918, ; 566: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 325
	i32 3597029428, ; 567: Xamarin.Android.Glide.GifDecoder.dll => 0xd6665034 => 214
	i32 3598340787, ; 568: System.Net.WebSockets.Client => 0xd67a52b3 => 78
	i32 3608519521, ; 569: System.Linq.dll => 0xd715a361 => 60
	i32 3624195450, ; 570: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 105
	i32 3627220390, ; 571: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 263
	i32 3633644679, ; 572: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 218
	i32 3638274909, ; 573: System.IO.FileSystem.Primitives.dll => 0xd8dbab5d => 48
	i32 3641597786, ; 574: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 249
	i32 3643446276, ; 575: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 322
	i32 3643854240, ; 576: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 260
	i32 3645089577, ; 577: System.ComponentModel.DataAnnotations => 0xd943a729 => 14
	i32 3657292374, ; 578: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 178
	i32 3660523487, ; 579: System.Net.NetworkInformation => 0xda2f27df => 67
	i32 3672681054, ; 580: Mono.Android.dll => 0xdae8aa5e => 170
	i32 3676670898, ; 581: Microsoft.Maui.Controls.HotReload.Forms => 0xdb258bb2 => 329
	i32 3682565725, ; 582: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 224
	i32 3684561358, ; 583: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 228
	i32 3697841164, ; 584: zh-Hant/Microsoft.Maui.Controls.resources.dll => 0xdc68940c => 327
	i32 3700591436, ; 585: Microsoft.IdentityModel.Abstractions.dll => 0xdc928b4c => 191
	i32 3700866549, ; 586: System.Net.WebProxy.dll => 0xdc96bdf5 => 77
	i32 3706696989, ; 587: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 233
	i32 3716563718, ; 588: System.Runtime.Intrinsics => 0xdd864306 => 107
	i32 3718780102, ; 589: Xamarin.AndroidX.Annotation => 0xdda814c6 => 217
	i32 3724971120, ; 590: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 259
	i32 3732100267, ; 591: System.Net.NameResolution => 0xde7354ab => 66
	i32 3737834244, ; 592: System.Net.Http.Json.dll => 0xdecad304 => 62
	i32 3748608112, ; 593: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 209
	i32 3751444290, ; 594: System.Xml.XPath => 0xdf9a7f42 => 159
	i32 3786282454, ; 595: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 226
	i32 3792276235, ; 596: System.Collections.NonGeneric => 0xe2098b0b => 10
	i32 3800979733, ; 597: Microsoft.Maui.Controls.Compatibility => 0xe28e5915 => 195
	i32 3802395368, ; 598: System.Collections.Specialized.dll => 0xe2a3f2e8 => 11
	i32 3817368567, ; 599: CommunityToolkit.Maui.dll => 0xe3886bf7 => 172
	i32 3819260425, ; 600: System.Net.WebProxy => 0xe3a54a09 => 77
	i32 3823082795, ; 601: System.Security.Cryptography.dll => 0xe3df9d2b => 125
	i32 3829621856, ; 602: System.Numerics.dll => 0xe4436460 => 82
	i32 3841636137, ; 603: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 181
	i32 3844307129, ; 604: System.Net.Mail.dll => 0xe52378b9 => 65
	i32 3849253459, ; 605: System.Runtime.InteropServices.dll => 0xe56ef253 => 106
	i32 3870376305, ; 606: System.Net.HttpListener.dll => 0xe6b14171 => 64
	i32 3873536506, ; 607: System.Security.Principal => 0xe6e179fa => 127
	i32 3875112723, ; 608: System.Security.Cryptography.Encoding.dll => 0xe6f98713 => 121
	i32 3885497537, ; 609: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 76
	i32 3885922214, ; 610: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 274
	i32 3888767677, ; 611: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 264
	i32 3889960447, ; 612: zh-Hans/Microsoft.Maui.Controls.resources.dll => 0xe7dc15ff => 326
	i32 3896106733, ; 613: System.Collections.Concurrent.dll => 0xe839deed => 8
	i32 3896760992, ; 614: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 232
	i32 3901907137, ; 615: Microsoft.VisualBasic.Core.dll => 0xe89260c1 => 2
	i32 3920810846, ; 616: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 43
	i32 3921031405, ; 617: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 277
	i32 3928044579, ; 618: System.Xml.ReaderWriter => 0xea213423 => 155
	i32 3930554604, ; 619: System.Security.Principal.dll => 0xea4780ec => 127
	i32 3931092270, ; 620: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 262
	i32 3945713374, ; 621: System.Data.DataSetExtensions.dll => 0xeb2ecede => 23
	i32 3953953790, ; 622: System.Text.Encoding.CodePages => 0xebac8bfe => 132
	i32 3955647286, ; 623: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 220
	i32 3959773229, ; 624: Xamarin.AndroidX.Lifecycle.Process => 0xec05582d => 251
	i32 3980434154, ; 625: th/Microsoft.Maui.Controls.resources.dll => 0xed409aea => 321
	i32 3982571493, ; 626: en-US/Syncfusion.Maui.Inputs.resources.dll => 0xed6137e5 => 328
	i32 3987592930, ; 627: he/Microsoft.Maui.Controls.resources.dll => 0xedadd6e2 => 303
	i32 4003436829, ; 628: System.Diagnostics.Process.dll => 0xee9f991d => 28
	i32 4015948917, ; 629: Xamarin.AndroidX.Annotation.Jvm.dll => 0xef5e8475 => 219
	i32 4019855520, ; 630: DigesettAPP.dll => 0xef9a20a0 => 0
	i32 4025784931, ; 631: System.Memory => 0xeff49a63 => 61
	i32 4046471985, ; 632: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 197
	i32 4054681211, ; 633: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 89
	i32 4068434129, ; 634: System.Private.Xml.Linq.dll => 0xf27f60d1 => 86
	i32 4073602200, ; 635: System.Threading.dll => 0xf2ce3c98 => 147
	i32 4094352644, ; 636: Microsoft.Maui.Essentials.dll => 0xf40add04 => 199
	i32 4099507663, ; 637: System.Drawing.dll => 0xf45985cf => 35
	i32 4100113165, ; 638: System.Private.Uri => 0xf462c30d => 85
	i32 4101593132, ; 639: Xamarin.AndroidX.Emoji2 => 0xf479582c => 240
	i32 4102112229, ; 640: pt/Microsoft.Maui.Controls.resources.dll => 0xf48143e5 => 316
	i32 4125707920, ; 641: ms/Microsoft.Maui.Controls.resources.dll => 0xf5e94e90 => 311
	i32 4126470640, ; 642: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 180
	i32 4127667938, ; 643: System.IO.FileSystem.Watcher => 0xf60736e2 => 49
	i32 4130442656, ; 644: System.AppContext => 0xf6318da0 => 6
	i32 4147896353, ; 645: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 89
	i32 4150914736, ; 646: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 323
	i32 4151237749, ; 647: System.Core => 0xf76edc75 => 21
	i32 4159265925, ; 648: System.Xml.XmlSerializer => 0xf7e95c85 => 161
	i32 4161255271, ; 649: System.Reflection.TypeExtensions => 0xf807b767 => 95
	i32 4164802419, ; 650: System.IO.FileSystem.Watcher.dll => 0xf83dd773 => 49
	i32 4181436372, ; 651: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 112
	i32 4182413190, ; 652: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 256
	i32 4182880526, ; 653: Microsoft.VisualStudio.DesignTools.MobileTapContracts => 0xf951b10e => 330
	i32 4185676441, ; 654: System.Security => 0xf97c5a99 => 129
	i32 4196529839, ; 655: System.Net.WebClient.dll => 0xfa21f6af => 75
	i32 4213026141, ; 656: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 209
	i32 4256097574, ; 657: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 233
	i32 4258378803, ; 658: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 0xfdd1b433 => 255
	i32 4260525087, ; 659: System.Buffers => 0xfdf2741f => 7
	i32 4263231520, ; 660: System.IdentityModel.Tokens.Jwt.dll => 0xfe1bc020 => 210
	i32 4271975918, ; 661: Microsoft.Maui.Controls.dll => 0xfea12dee => 196
	i32 4274623895, ; 662: CommunityToolkit.Mvvm.dll => 0xfec99597 => 174
	i32 4274976490, ; 663: System.Runtime.Numerics => 0xfecef6ea => 109
	i32 4292120959, ; 664: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 256
	i32 4294763496 ; 665: Xamarin.AndroidX.ExifInterface.dll => 0xfffce3e8 => 242
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [666 x i32] [
	i32 67, ; 0
	i32 66, ; 1
	i32 107, ; 2
	i32 252, ; 3
	i32 286, ; 4
	i32 47, ; 5
	i32 201, ; 6
	i32 79, ; 7
	i32 144, ; 8
	i32 29, ; 9
	i32 327, ; 10
	i32 123, ; 11
	i32 200, ; 12
	i32 101, ; 13
	i32 183, ; 14
	i32 270, ; 15
	i32 106, ; 16
	i32 270, ; 17
	i32 138, ; 18
	i32 290, ; 19
	i32 76, ; 20
	i32 123, ; 21
	i32 13, ; 22
	i32 226, ; 23
	i32 203, ; 24
	i32 131, ; 25
	i32 205, ; 26
	i32 272, ; 27
	i32 150, ; 28
	i32 324, ; 29
	i32 325, ; 30
	i32 18, ; 31
	i32 224, ; 32
	i32 26, ; 33
	i32 182, ; 34
	i32 246, ; 35
	i32 1, ; 36
	i32 58, ; 37
	i32 41, ; 38
	i32 90, ; 39
	i32 229, ; 40
	i32 146, ; 41
	i32 248, ; 42
	i32 245, ; 43
	i32 296, ; 44
	i32 53, ; 45
	i32 184, ; 46
	i32 68, ; 47
	i32 205, ; 48
	i32 324, ; 49
	i32 215, ; 50
	i32 82, ; 51
	i32 309, ; 52
	i32 247, ; 53
	i32 308, ; 54
	i32 130, ; 55
	i32 54, ; 56
	i32 148, ; 57
	i32 73, ; 58
	i32 144, ; 59
	i32 176, ; 60
	i32 61, ; 61
	i32 145, ; 62
	i32 332, ; 63
	i32 164, ; 64
	i32 320, ; 65
	i32 230, ; 66
	i32 12, ; 67
	i32 243, ; 68
	i32 124, ; 69
	i32 151, ; 70
	i32 206, ; 71
	i32 112, ; 72
	i32 165, ; 73
	i32 163, ; 74
	i32 245, ; 75
	i32 191, ; 76
	i32 258, ; 77
	i32 83, ; 78
	i32 307, ; 79
	i32 301, ; 80
	i32 190, ; 81
	i32 149, ; 82
	i32 290, ; 83
	i32 59, ; 84
	i32 185, ; 85
	i32 50, ; 86
	i32 102, ; 87
	i32 113, ; 88
	i32 39, ; 89
	i32 283, ; 90
	i32 281, ; 91
	i32 119, ; 92
	i32 315, ; 93
	i32 172, ; 94
	i32 51, ; 95
	i32 43, ; 96
	i32 118, ; 97
	i32 235, ; 98
	i32 313, ; 99
	i32 241, ; 100
	i32 80, ; 101
	i32 135, ; 102
	i32 277, ; 103
	i32 222, ; 104
	i32 8, ; 105
	i32 72, ; 106
	i32 295, ; 107
	i32 154, ; 108
	i32 292, ; 109
	i32 153, ; 110
	i32 91, ; 111
	i32 287, ; 112
	i32 44, ; 113
	i32 310, ; 114
	i32 298, ; 115
	i32 291, ; 116
	i32 108, ; 117
	i32 189, ; 118
	i32 128, ; 119
	i32 25, ; 120
	i32 212, ; 121
	i32 71, ; 122
	i32 54, ; 123
	i32 45, ; 124
	i32 319, ; 125
	i32 188, ; 126
	i32 236, ; 127
	i32 22, ; 128
	i32 250, ; 129
	i32 85, ; 130
	i32 42, ; 131
	i32 159, ; 132
	i32 70, ; 133
	i32 263, ; 134
	i32 3, ; 135
	i32 41, ; 136
	i32 62, ; 137
	i32 16, ; 138
	i32 52, ; 139
	i32 322, ; 140
	i32 286, ; 141
	i32 104, ; 142
	i32 201, ; 143
	i32 291, ; 144
	i32 284, ; 145
	i32 247, ; 146
	i32 33, ; 147
	i32 157, ; 148
	i32 84, ; 149
	i32 31, ; 150
	i32 12, ; 151
	i32 50, ; 152
	i32 55, ; 153
	i32 267, ; 154
	i32 35, ; 155
	i32 181, ; 156
	i32 297, ; 157
	i32 285, ; 158
	i32 220, ; 159
	i32 34, ; 160
	i32 57, ; 161
	i32 183, ; 162
	i32 254, ; 163
	i32 175, ; 164
	i32 17, ; 165
	i32 288, ; 166
	i32 163, ; 167
	i32 310, ; 168
	i32 253, ; 169
	i32 187, ; 170
	i32 280, ; 171
	i32 176, ; 172
	i32 316, ; 173
	i32 152, ; 174
	i32 276, ; 175
	i32 261, ; 176
	i32 314, ; 177
	i32 222, ; 178
	i32 28, ; 179
	i32 174, ; 180
	i32 51, ; 181
	i32 312, ; 182
	i32 281, ; 183
	i32 5, ; 184
	i32 296, ; 185
	i32 271, ; 186
	i32 275, ; 187
	i32 227, ; 188
	i32 292, ; 189
	i32 219, ; 190
	i32 238, ; 191
	i32 84, ; 192
	i32 280, ; 193
	i32 60, ; 194
	i32 111, ; 195
	i32 56, ; 196
	i32 326, ; 197
	i32 267, ; 198
	i32 98, ; 199
	i32 19, ; 200
	i32 231, ; 201
	i32 110, ; 202
	i32 100, ; 203
	i32 101, ; 204
	i32 294, ; 205
	i32 103, ; 206
	i32 284, ; 207
	i32 70, ; 208
	i32 37, ; 209
	i32 31, ; 210
	i32 102, ; 211
	i32 72, ; 212
	i32 210, ; 213
	i32 300, ; 214
	i32 9, ; 215
	i32 122, ; 216
	i32 45, ; 217
	i32 221, ; 218
	i32 190, ; 219
	i32 9, ; 220
	i32 42, ; 221
	i32 4, ; 222
	i32 268, ; 223
	i32 304, ; 224
	i32 192, ; 225
	i32 184, ; 226
	i32 299, ; 227
	i32 30, ; 228
	i32 137, ; 229
	i32 91, ; 230
	i32 92, ; 231
	i32 319, ; 232
	i32 48, ; 233
	i32 140, ; 234
	i32 111, ; 235
	i32 139, ; 236
	i32 237, ; 237
	i32 114, ; 238
	i32 285, ; 239
	i32 156, ; 240
	i32 329, ; 241
	i32 75, ; 242
	i32 78, ; 243
	i32 257, ; 244
	i32 36, ; 245
	i32 279, ; 246
	i32 173, ; 247
	i32 241, ; 248
	i32 234, ; 249
	i32 63, ; 250
	i32 137, ; 251
	i32 15, ; 252
	i32 207, ; 253
	i32 115, ; 254
	i32 273, ; 255
	i32 282, ; 256
	i32 229, ; 257
	i32 47, ; 258
	i32 69, ; 259
	i32 79, ; 260
	i32 125, ; 261
	i32 93, ; 262
	i32 120, ; 263
	i32 289, ; 264
	i32 26, ; 265
	i32 250, ; 266
	i32 96, ; 267
	i32 27, ; 268
	i32 225, ; 269
	i32 317, ; 270
	i32 295, ; 271
	i32 148, ; 272
	i32 168, ; 273
	i32 4, ; 274
	i32 97, ; 275
	i32 32, ; 276
	i32 92, ; 277
	i32 272, ; 278
	i32 185, ; 279
	i32 21, ; 280
	i32 40, ; 281
	i32 169, ; 282
	i32 311, ; 283
	i32 243, ; 284
	i32 303, ; 285
	i32 257, ; 286
	i32 288, ; 287
	i32 282, ; 288
	i32 262, ; 289
	i32 2, ; 290
	i32 133, ; 291
	i32 110, ; 292
	i32 331, ; 293
	i32 0, ; 294
	i32 186, ; 295
	i32 323, ; 296
	i32 212, ; 297
	i32 320, ; 298
	i32 57, ; 299
	i32 94, ; 300
	i32 302, ; 301
	i32 38, ; 302
	i32 223, ; 303
	i32 331, ; 304
	i32 25, ; 305
	i32 93, ; 306
	i32 88, ; 307
	i32 98, ; 308
	i32 10, ; 309
	i32 86, ; 310
	i32 99, ; 311
	i32 269, ; 312
	i32 177, ; 313
	i32 289, ; 314
	i32 214, ; 315
	i32 194, ; 316
	i32 299, ; 317
	i32 7, ; 318
	i32 254, ; 319
	i32 294, ; 320
	i32 211, ; 321
	i32 87, ; 322
	i32 179, ; 323
	i32 249, ; 324
	i32 153, ; 325
	i32 298, ; 326
	i32 32, ; 327
	i32 115, ; 328
	i32 81, ; 329
	i32 20, ; 330
	i32 11, ; 331
	i32 161, ; 332
	i32 3, ; 333
	i32 198, ; 334
	i32 306, ; 335
	i32 188, ; 336
	i32 186, ; 337
	i32 83, ; 338
	i32 293, ; 339
	i32 63, ; 340
	i32 308, ; 341
	i32 276, ; 342
	i32 142, ; 343
	i32 258, ; 344
	i32 156, ; 345
	i32 40, ; 346
	i32 116, ; 347
	i32 178, ; 348
	i32 213, ; 349
	i32 302, ; 350
	i32 265, ; 351
	i32 130, ; 352
	i32 74, ; 353
	i32 65, ; 354
	i32 312, ; 355
	i32 171, ; 356
	i32 217, ; 357
	i32 142, ; 358
	i32 105, ; 359
	i32 150, ; 360
	i32 69, ; 361
	i32 202, ; 362
	i32 155, ; 363
	i32 193, ; 364
	i32 177, ; 365
	i32 120, ; 366
	i32 126, ; 367
	i32 307, ; 368
	i32 151, ; 369
	i32 240, ; 370
	i32 330, ; 371
	i32 140, ; 372
	i32 227, ; 373
	i32 328, ; 374
	i32 304, ; 375
	i32 20, ; 376
	i32 14, ; 377
	i32 134, ; 378
	i32 74, ; 379
	i32 58, ; 380
	i32 230, ; 381
	i32 166, ; 382
	i32 167, ; 383
	i32 196, ; 384
	i32 15, ; 385
	i32 73, ; 386
	i32 6, ; 387
	i32 23, ; 388
	i32 252, ; 389
	i32 211, ; 390
	i32 90, ; 391
	i32 305, ; 392
	i32 1, ; 393
	i32 135, ; 394
	i32 253, ; 395
	i32 275, ; 396
	i32 133, ; 397
	i32 68, ; 398
	i32 145, ; 399
	i32 314, ; 400
	i32 293, ; 401
	i32 244, ; 402
	i32 187, ; 403
	i32 87, ; 404
	i32 95, ; 405
	i32 234, ; 406
	i32 239, ; 407
	i32 309, ; 408
	i32 30, ; 409
	i32 44, ; 410
	i32 248, ; 411
	i32 193, ; 412
	i32 213, ; 413
	i32 108, ; 414
	i32 157, ; 415
	i32 34, ; 416
	i32 22, ; 417
	i32 113, ; 418
	i32 56, ; 419
	i32 273, ; 420
	i32 143, ; 421
	i32 117, ; 422
	i32 119, ; 423
	i32 109, ; 424
	i32 215, ; 425
	i32 138, ; 426
	i32 221, ; 427
	i32 53, ; 428
	i32 204, ; 429
	i32 104, ; 430
	i32 315, ; 431
	i32 197, ; 432
	i32 198, ; 433
	i32 132, ; 434
	i32 287, ; 435
	i32 278, ; 436
	i32 266, ; 437
	i32 321, ; 438
	i32 244, ; 439
	i32 200, ; 440
	i32 158, ; 441
	i32 300, ; 442
	i32 231, ; 443
	i32 162, ; 444
	i32 131, ; 445
	i32 266, ; 446
	i32 160, ; 447
	i32 313, ; 448
	i32 255, ; 449
	i32 139, ; 450
	i32 278, ; 451
	i32 274, ; 452
	i32 168, ; 453
	i32 199, ; 454
	i32 173, ; 455
	i32 202, ; 456
	i32 216, ; 457
	i32 283, ; 458
	i32 39, ; 459
	i32 242, ; 460
	i32 80, ; 461
	i32 55, ; 462
	i32 36, ; 463
	i32 96, ; 464
	i32 165, ; 465
	i32 171, ; 466
	i32 279, ; 467
	i32 81, ; 468
	i32 218, ; 469
	i32 97, ; 470
	i32 29, ; 471
	i32 158, ; 472
	i32 18, ; 473
	i32 126, ; 474
	i32 189, ; 475
	i32 118, ; 476
	i32 238, ; 477
	i32 269, ; 478
	i32 251, ; 479
	i32 271, ; 480
	i32 182, ; 481
	i32 164, ; 482
	i32 246, ; 483
	i32 332, ; 484
	i32 268, ; 485
	i32 259, ; 486
	i32 169, ; 487
	i32 16, ; 488
	i32 143, ; 489
	i32 306, ; 490
	i32 194, ; 491
	i32 124, ; 492
	i32 117, ; 493
	i32 37, ; 494
	i32 114, ; 495
	i32 46, ; 496
	i32 141, ; 497
	i32 116, ; 498
	i32 208, ; 499
	i32 33, ; 500
	i32 203, ; 501
	i32 175, ; 502
	i32 94, ; 503
	i32 52, ; 504
	i32 208, ; 505
	i32 260, ; 506
	i32 128, ; 507
	i32 152, ; 508
	i32 24, ; 509
	i32 160, ; 510
	i32 237, ; 511
	i32 147, ; 512
	i32 103, ; 513
	i32 88, ; 514
	i32 225, ; 515
	i32 59, ; 516
	i32 141, ; 517
	i32 99, ; 518
	i32 5, ; 519
	i32 13, ; 520
	i32 121, ; 521
	i32 134, ; 522
	i32 27, ; 523
	i32 301, ; 524
	i32 207, ; 525
	i32 192, ; 526
	i32 71, ; 527
	i32 235, ; 528
	i32 24, ; 529
	i32 223, ; 530
	i32 264, ; 531
	i32 261, ; 532
	i32 318, ; 533
	i32 136, ; 534
	i32 216, ; 535
	i32 232, ; 536
	i32 167, ; 537
	i32 265, ; 538
	i32 297, ; 539
	i32 206, ; 540
	i32 100, ; 541
	i32 122, ; 542
	i32 236, ; 543
	i32 179, ; 544
	i32 180, ; 545
	i32 162, ; 546
	i32 166, ; 547
	i32 239, ; 548
	i32 38, ; 549
	i32 195, ; 550
	i32 305, ; 551
	i32 17, ; 552
	i32 170, ; 553
	i32 318, ; 554
	i32 317, ; 555
	i32 136, ; 556
	i32 149, ; 557
	i32 228, ; 558
	i32 154, ; 559
	i32 129, ; 560
	i32 19, ; 561
	i32 64, ; 562
	i32 204, ; 563
	i32 146, ; 564
	i32 46, ; 565
	i32 325, ; 566
	i32 214, ; 567
	i32 78, ; 568
	i32 60, ; 569
	i32 105, ; 570
	i32 263, ; 571
	i32 218, ; 572
	i32 48, ; 573
	i32 249, ; 574
	i32 322, ; 575
	i32 260, ; 576
	i32 14, ; 577
	i32 178, ; 578
	i32 67, ; 579
	i32 170, ; 580
	i32 329, ; 581
	i32 224, ; 582
	i32 228, ; 583
	i32 327, ; 584
	i32 191, ; 585
	i32 77, ; 586
	i32 233, ; 587
	i32 107, ; 588
	i32 217, ; 589
	i32 259, ; 590
	i32 66, ; 591
	i32 62, ; 592
	i32 209, ; 593
	i32 159, ; 594
	i32 226, ; 595
	i32 10, ; 596
	i32 195, ; 597
	i32 11, ; 598
	i32 172, ; 599
	i32 77, ; 600
	i32 125, ; 601
	i32 82, ; 602
	i32 181, ; 603
	i32 65, ; 604
	i32 106, ; 605
	i32 64, ; 606
	i32 127, ; 607
	i32 121, ; 608
	i32 76, ; 609
	i32 274, ; 610
	i32 264, ; 611
	i32 326, ; 612
	i32 8, ; 613
	i32 232, ; 614
	i32 2, ; 615
	i32 43, ; 616
	i32 277, ; 617
	i32 155, ; 618
	i32 127, ; 619
	i32 262, ; 620
	i32 23, ; 621
	i32 132, ; 622
	i32 220, ; 623
	i32 251, ; 624
	i32 321, ; 625
	i32 328, ; 626
	i32 303, ; 627
	i32 28, ; 628
	i32 219, ; 629
	i32 0, ; 630
	i32 61, ; 631
	i32 197, ; 632
	i32 89, ; 633
	i32 86, ; 634
	i32 147, ; 635
	i32 199, ; 636
	i32 35, ; 637
	i32 85, ; 638
	i32 240, ; 639
	i32 316, ; 640
	i32 311, ; 641
	i32 180, ; 642
	i32 49, ; 643
	i32 6, ; 644
	i32 89, ; 645
	i32 323, ; 646
	i32 21, ; 647
	i32 161, ; 648
	i32 95, ; 649
	i32 49, ; 650
	i32 112, ; 651
	i32 256, ; 652
	i32 330, ; 653
	i32 129, ; 654
	i32 75, ; 655
	i32 209, ; 656
	i32 233, ; 657
	i32 255, ; 658
	i32 7, ; 659
	i32 210, ; 660
	i32 196, ; 661
	i32 174, ; 662
	i32 109, ; 663
	i32 256, ; 664
	i32 242 ; 665
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
