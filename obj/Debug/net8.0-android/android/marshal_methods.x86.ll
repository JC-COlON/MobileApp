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

@assembly_image_cache = dso_local local_unnamed_addr global [337 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [668 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 67
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 66
	i32 15721112, ; 2: System.Runtime.Intrinsics.dll => 0xefe298 => 107
	i32 32687329, ; 3: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 253
	i32 34715100, ; 4: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 287
	i32 34839235, ; 5: System.IO.FileSystem.DriveInfo => 0x2139ac3 => 47
	i32 39109920, ; 6: Newtonsoft.Json.dll => 0x254c520 => 201
	i32 39485524, ; 7: System.Net.WebSockets.dll => 0x25a8054 => 79
	i32 42639949, ; 8: System.Threading.Thread => 0x28aa24d => 144
	i32 66541672, ; 9: System.Diagnostics.StackTrace => 0x3f75868 => 29
	i32 67008169, ; 10: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 328
	i32 68219467, ; 11: System.Security.Cryptography.Primitives => 0x410f24b => 123
	i32 72070932, ; 12: Microsoft.Maui.Graphics.dll => 0x44bb714 => 200
	i32 82292897, ; 13: System.Runtime.CompilerServices.VisualC.dll => 0x4e7b0a1 => 101
	i32 98325684, ; 14: Microsoft.Extensions.Diagnostics.Abstractions => 0x5dc54b4 => 183
	i32 101534019, ; 15: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 271
	i32 117431740, ; 16: System.Runtime.InteropServices => 0x6ffddbc => 106
	i32 120558881, ; 17: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 271
	i32 122350210, ; 18: System.Threading.Channels.dll => 0x74aea82 => 138
	i32 134690465, ; 19: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 291
	i32 142721839, ; 20: System.Net.WebHeaderCollection => 0x881c32f => 76
	i32 149972175, ; 21: System.Security.Cryptography.Primitives.dll => 0x8f064cf => 123
	i32 159306688, ; 22: System.ComponentModel.Annotations => 0x97ed3c0 => 13
	i32 165246403, ; 23: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 227
	i32 172961045, ; 24: Syncfusion.Maui.Core.dll => 0xa4f2d15 => 203
	i32 176265551, ; 25: System.ServiceProcess => 0xa81994f => 131
	i32 179457679, ; 26: Syncfusion.Maui.Expander => 0xab24e8f => 205
	i32 182336117, ; 27: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 273
	i32 184328833, ; 28: System.ValueTuple.dll => 0xafca281 => 150
	i32 195452805, ; 29: vi/Microsoft.Maui.Controls.resources.dll => 0xba65f85 => 325
	i32 199333315, ; 30: zh-HK/Microsoft.Maui.Controls.resources.dll => 0xbe195c3 => 326
	i32 205061960, ; 31: System.ComponentModel => 0xc38ff48 => 18
	i32 209399409, ; 32: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 225
	i32 220171995, ; 33: System.Diagnostics.Debug => 0xd1f8edb => 26
	i32 221958352, ; 34: Microsoft.Extensions.Diagnostics.dll => 0xd3ad0d0 => 182
	i32 230216969, ; 35: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 247
	i32 230752869, ; 36: Microsoft.CSharp.dll => 0xdc10265 => 1
	i32 231409092, ; 37: System.Linq.Parallel => 0xdcb05c4 => 58
	i32 231814094, ; 38: System.Globalization => 0xdd133ce => 41
	i32 246610117, ; 39: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 90
	i32 261689757, ; 40: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 230
	i32 276479776, ; 41: System.Threading.Timer.dll => 0x107abf20 => 146
	i32 278686392, ; 42: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 249
	i32 280482487, ; 43: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 246
	i32 280992041, ; 44: cs/Microsoft.Maui.Controls.resources.dll => 0x10bf9929 => 297
	i32 291076382, ; 45: System.IO.Pipes.AccessControl.dll => 0x1159791e => 53
	i32 291275502, ; 46: Microsoft.Extensions.Http.dll => 0x115c82ee => 184
	i32 298918909, ; 47: System.Net.Ping.dll => 0x11d123fd => 68
	i32 315399044, ; 48: Syncfusion.Maui.Expander.dll => 0x12cc9b84 => 205
	i32 317674968, ; 49: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 325
	i32 318968648, ; 50: Xamarin.AndroidX.Activity.dll => 0x13031348 => 216
	i32 321597661, ; 51: System.Numerics => 0x132b30dd => 82
	i32 336156722, ; 52: ja/Microsoft.Maui.Controls.resources.dll => 0x14095832 => 310
	i32 342366114, ; 53: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 248
	i32 356389973, ; 54: it/Microsoft.Maui.Controls.resources.dll => 0x153e1455 => 309
	i32 360082299, ; 55: System.ServiceModel.Web => 0x15766b7b => 130
	i32 367780167, ; 56: System.IO.Pipes => 0x15ebe147 => 54
	i32 374376850, ; 57: Syncfusion.Maui.Popup.dll => 0x16508992 => 209
	i32 374914964, ; 58: System.Transactions.Local => 0x1658bf94 => 148
	i32 375677976, ; 59: System.Net.ServicePoint.dll => 0x16646418 => 73
	i32 379916513, ; 60: System.Threading.Thread.dll => 0x16a510e1 => 144
	i32 382771021, ; 61: Microsoft.Bcl.Memory.dll => 0x16d09f4d => 176
	i32 385762202, ; 62: System.Memory.dll => 0x16fe439a => 61
	i32 392610295, ; 63: System.Threading.ThreadPool.dll => 0x1766c1f7 => 145
	i32 395744057, ; 64: _Microsoft.Android.Resource.Designer => 0x17969339 => 333
	i32 403441872, ; 65: WindowsBase => 0x180c08d0 => 164
	i32 435591531, ; 66: sv/Microsoft.Maui.Controls.resources.dll => 0x19f6996b => 321
	i32 441335492, ; 67: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 231
	i32 442565967, ; 68: System.Collections => 0x1a61054f => 12
	i32 450948140, ; 69: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 244
	i32 451504562, ; 70: System.Security.Cryptography.X509Certificates => 0x1ae969b2 => 124
	i32 456227837, ; 71: System.Web.HttpUtility.dll => 0x1b317bfd => 151
	i32 456681651, ; 72: Syncfusion.Maui.GridCommon.dll => 0x1b3868b3 => 206
	i32 459347974, ; 73: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 112
	i32 465846621, ; 74: mscorlib => 0x1bc4415d => 165
	i32 469710990, ; 75: System.dll => 0x1bff388e => 163
	i32 476646585, ; 76: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 246
	i32 485463106, ; 77: Microsoft.IdentityModel.Abstractions => 0x1cef9442 => 191
	i32 486930444, ; 78: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 259
	i32 498788369, ; 79: System.ObjectModel => 0x1dbae811 => 83
	i32 500358224, ; 80: id/Microsoft.Maui.Controls.resources.dll => 0x1dd2dc50 => 308
	i32 503918385, ; 81: fi/Microsoft.Maui.Controls.resources.dll => 0x1e092f31 => 302
	i32 513247710, ; 82: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 190
	i32 526420162, ; 83: System.Transactions.dll => 0x1f6088c2 => 149
	i32 527452488, ; 84: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 291
	i32 530272170, ; 85: System.Linq.Queryable => 0x1f9b4faa => 59
	i32 539058512, ; 86: Microsoft.Extensions.Logging => 0x20216150 => 185
	i32 540030774, ; 87: System.IO.FileSystem.dll => 0x20303736 => 50
	i32 545304856, ; 88: System.Runtime.Extensions => 0x2080b118 => 102
	i32 546455878, ; 89: System.Runtime.Serialization.Xml => 0x20924146 => 113
	i32 549171840, ; 90: System.Globalization.Calendars => 0x20bbb280 => 39
	i32 557405415, ; 91: Jsr305Binding => 0x213954e7 => 284
	i32 569601784, ; 92: Xamarin.AndroidX.Window.Extensions.Core.Core => 0x21f36ef8 => 282
	i32 577335427, ; 93: System.Security.Cryptography.Cng => 0x22697083 => 119
	i32 592146354, ; 94: pt-BR/Microsoft.Maui.Controls.resources.dll => 0x234b6fb2 => 316
	i32 597488923, ; 95: CommunityToolkit.Maui => 0x239cf51b => 172
	i32 601371474, ; 96: System.IO.IsolatedStorage.dll => 0x23d83352 => 51
	i32 605376203, ; 97: System.IO.Compression.FileSystem => 0x24154ecb => 43
	i32 613668793, ; 98: System.Security.Cryptography.Algorithms => 0x2493d7b9 => 118
	i32 627609679, ; 99: Xamarin.AndroidX.CustomView => 0x2568904f => 236
	i32 627931235, ; 100: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 314
	i32 639843206, ; 101: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x26233b86 => 242
	i32 643868501, ; 102: System.Net => 0x2660a755 => 80
	i32 662205335, ; 103: System.Text.Encodings.Web.dll => 0x27787397 => 135
	i32 663517072, ; 104: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 278
	i32 666292255, ; 105: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 223
	i32 672442732, ; 106: System.Collections.Concurrent => 0x2814a96c => 8
	i32 683518922, ; 107: System.Net.Security => 0x28bdabca => 72
	i32 688181140, ; 108: ca/Microsoft.Maui.Controls.resources.dll => 0x2904cf94 => 296
	i32 690569205, ; 109: System.Xml.Linq.dll => 0x29293ff5 => 154
	i32 691348768, ; 110: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 293
	i32 693804605, ; 111: System.Windows => 0x295a9e3d => 153
	i32 695450347, ; 112: Syncfusion.Maui.Popup => 0x2973baeb => 209
	i32 699345723, ; 113: System.Reflection.Emit => 0x29af2b3b => 91
	i32 700284507, ; 114: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 288
	i32 700358131, ; 115: System.IO.Compression.ZipFile => 0x29be9df3 => 44
	i32 706645707, ; 116: ko/Microsoft.Maui.Controls.resources.dll => 0x2a1e8ecb => 311
	i32 709557578, ; 117: de/Microsoft.Maui.Controls.resources.dll => 0x2a4afd4a => 299
	i32 720511267, ; 118: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 292
	i32 722857257, ; 119: System.Runtime.Loader.dll => 0x2b15ed29 => 108
	i32 731701662, ; 120: Microsoft.Extensions.Options.ConfigurationExtensions => 0x2b9ce19e => 189
	i32 735137430, ; 121: System.Security.SecureString.dll => 0x2bd14e96 => 128
	i32 752232764, ; 122: System.Diagnostics.Contracts.dll => 0x2cd6293c => 25
	i32 755313932, ; 123: Xamarin.Android.Glide.Annotations.dll => 0x2d052d0c => 213
	i32 759454413, ; 124: System.Net.Requests => 0x2d445acd => 71
	i32 762598435, ; 125: System.IO.Pipes.dll => 0x2d745423 => 54
	i32 775507847, ; 126: System.IO.Compression => 0x2e394f87 => 45
	i32 777317022, ; 127: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 320
	i32 789151979, ; 128: Microsoft.Extensions.Options => 0x2f0980eb => 188
	i32 790371945, ; 129: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 0x2f1c1e69 => 237
	i32 804715423, ; 130: System.Data.Common => 0x2ff6fb9f => 22
	i32 807930345, ; 131: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 0x302809e9 => 251
	i32 823281589, ; 132: System.Private.Uri.dll => 0x311247b5 => 85
	i32 830298997, ; 133: System.IO.Compression.Brotli => 0x317d5b75 => 42
	i32 832635846, ; 134: System.Xml.XPath.dll => 0x31a103c6 => 159
	i32 834051424, ; 135: System.Net.Quic => 0x31b69d60 => 70
	i32 843511501, ; 136: Xamarin.AndroidX.Print => 0x3246f6cd => 264
	i32 873119928, ; 137: Microsoft.VisualBasic => 0x340ac0b8 => 3
	i32 877678880, ; 138: System.Globalization.dll => 0x34505120 => 41
	i32 878954865, ; 139: System.Net.Http.Json => 0x3463c971 => 62
	i32 904024072, ; 140: System.ComponentModel.Primitives.dll => 0x35e25008 => 16
	i32 911108515, ; 141: System.IO.MemoryMappedFiles.dll => 0x364e69a3 => 52
	i32 926902833, ; 142: tr/Microsoft.Maui.Controls.resources.dll => 0x373f6a31 => 323
	i32 928116545, ; 143: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 287
	i32 952186615, ; 144: System.Runtime.InteropServices.JavaScript.dll => 0x38c136f7 => 104
	i32 955402788, ; 145: Newtonsoft.Json => 0x38f24a24 => 201
	i32 956575887, ; 146: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 292
	i32 966729478, ; 147: Xamarin.Google.Crypto.Tink.Android => 0x399f1f06 => 285
	i32 967690846, ; 148: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 248
	i32 975236339, ; 149: System.Diagnostics.Tracing => 0x3a20ecf3 => 33
	i32 975874589, ; 150: System.Xml.XDocument => 0x3a2aaa1d => 157
	i32 986514023, ; 151: System.Private.DataContractSerialization.dll => 0x3acd0267 => 84
	i32 987214855, ; 152: System.Diagnostics.Tools => 0x3ad7b407 => 31
	i32 992768348, ; 153: System.Collections.dll => 0x3b2c715c => 12
	i32 994442037, ; 154: System.IO.FileSystem => 0x3b45fb35 => 50
	i32 1001831731, ; 155: System.IO.UnmanagedMemoryStream.dll => 0x3bb6bd33 => 55
	i32 1012816738, ; 156: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 268
	i32 1019214401, ; 157: System.Drawing => 0x3cbffa41 => 35
	i32 1028951442, ; 158: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 181
	i32 1029334545, ; 159: da/Microsoft.Maui.Controls.resources.dll => 0x3d5a6611 => 298
	i32 1031528504, ; 160: Xamarin.Google.ErrorProne.Annotations.dll => 0x3d7be038 => 286
	i32 1035644815, ; 161: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 221
	i32 1036536393, ; 162: System.Drawing.Primitives.dll => 0x3dc84a49 => 34
	i32 1044663988, ; 163: System.Linq.Expressions.dll => 0x3e444eb4 => 57
	i32 1048992957, ; 164: Microsoft.Extensions.Diagnostics.Abstractions.dll => 0x3e865cbd => 183
	i32 1052210849, ; 165: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 255
	i32 1067306892, ; 166: GoogleGson => 0x3f9dcf8c => 175
	i32 1082857460, ; 167: System.ComponentModel.TypeConverter => 0x408b17f4 => 17
	i32 1084122840, ; 168: Xamarin.Kotlin.StdLib => 0x409e66d8 => 289
	i32 1098259244, ; 169: System => 0x41761b2c => 163
	i32 1118262833, ; 170: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 311
	i32 1121599056, ; 171: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 0x42da3e50 => 254
	i32 1127624469, ; 172: Microsoft.Extensions.Logging.Debug => 0x43362f15 => 187
	i32 1149092582, ; 173: Xamarin.AndroidX.Window => 0x447dc2e6 => 281
	i32 1162065116, ; 174: Microsoft.Bcl.Memory => 0x4543b4dc => 176
	i32 1168523401, ; 175: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 317
	i32 1170634674, ; 176: System.Web.dll => 0x45c677b2 => 152
	i32 1175144683, ; 177: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 277
	i32 1178241025, ; 178: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 262
	i32 1203215381, ; 179: pl/Microsoft.Maui.Controls.resources.dll => 0x47b79c15 => 315
	i32 1204270330, ; 180: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 223
	i32 1208641965, ; 181: System.Diagnostics.Process => 0x480a69ad => 28
	i32 1214827643, ; 182: CommunityToolkit.Mvvm => 0x4868cc7b => 174
	i32 1219128291, ; 183: System.IO.IsolatedStorage => 0x48aa6be3 => 51
	i32 1234928153, ; 184: nb/Microsoft.Maui.Controls.resources.dll => 0x499b8219 => 313
	i32 1243150071, ; 185: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 0x4a18f6f7 => 282
	i32 1253011324, ; 186: Microsoft.Win32.Registry => 0x4aaf6f7c => 5
	i32 1260983243, ; 187: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 297
	i32 1264511973, ; 188: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 272
	i32 1267360935, ; 189: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 276
	i32 1273260888, ; 190: Xamarin.AndroidX.Collection.Ktx => 0x4be46b58 => 228
	i32 1275534314, ; 191: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 293
	i32 1278448581, ; 192: Xamarin.AndroidX.Annotation.Jvm => 0x4c3393c5 => 220
	i32 1293217323, ; 193: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 239
	i32 1309188875, ; 194: System.Private.DataContractSerialization => 0x4e08a30b => 84
	i32 1322716291, ; 195: Xamarin.AndroidX.Window.dll => 0x4ed70c83 => 281
	i32 1324164729, ; 196: System.Linq => 0x4eed2679 => 60
	i32 1335329327, ; 197: System.Runtime.Serialization.Json.dll => 0x4f97822f => 111
	i32 1364015309, ; 198: System.IO => 0x514d38cd => 56
	i32 1373134921, ; 199: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 327
	i32 1376866003, ; 200: Xamarin.AndroidX.SavedState => 0x52114ed3 => 268
	i32 1379779777, ; 201: System.Resources.ResourceManager => 0x523dc4c1 => 98
	i32 1402170036, ; 202: System.Configuration.dll => 0x53936ab4 => 19
	i32 1406073936, ; 203: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 232
	i32 1408764838, ; 204: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 110
	i32 1411638395, ; 205: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 100
	i32 1422545099, ; 206: System.Runtime.CompilerServices.VisualC => 0x54ca50cb => 101
	i32 1430672901, ; 207: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 295
	i32 1434145427, ; 208: System.Runtime.Handles => 0x557b5293 => 103
	i32 1435222561, ; 209: Xamarin.Google.Crypto.Tink.Android.dll => 0x558bc221 => 285
	i32 1439761251, ; 210: System.Net.Quic.dll => 0x55d10363 => 70
	i32 1452070440, ; 211: System.Formats.Asn1.dll => 0x568cd628 => 37
	i32 1453312822, ; 212: System.Diagnostics.Tools.dll => 0x569fcb36 => 31
	i32 1457743152, ; 213: System.Runtime.Extensions.dll => 0x56e36530 => 102
	i32 1458022317, ; 214: System.Net.Security.dll => 0x56e7a7ad => 72
	i32 1460893475, ; 215: System.IdentityModel.Tokens.Jwt => 0x57137723 => 211
	i32 1461004990, ; 216: es\Microsoft.Maui.Controls.resources => 0x57152abe => 301
	i32 1461234159, ; 217: System.Collections.Immutable.dll => 0x5718a9ef => 9
	i32 1461719063, ; 218: System.Security.Cryptography.OpenSsl => 0x57201017 => 122
	i32 1462112819, ; 219: System.IO.Compression.dll => 0x57261233 => 45
	i32 1469204771, ; 220: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 222
	i32 1470490898, ; 221: Microsoft.Extensions.Primitives => 0x57a5e912 => 190
	i32 1479771757, ; 222: System.Collections.Immutable => 0x5833866d => 9
	i32 1480492111, ; 223: System.IO.Compression.Brotli.dll => 0x583e844f => 42
	i32 1487239319, ; 224: Microsoft.Win32.Primitives => 0x58a57897 => 4
	i32 1490025113, ; 225: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 0x58cffa99 => 269
	i32 1493001747, ; 226: hi/Microsoft.Maui.Controls.resources.dll => 0x58fd6613 => 305
	i32 1498168481, ; 227: Microsoft.IdentityModel.JsonWebTokens.dll => 0x594c3ca1 => 192
	i32 1505131794, ; 228: Microsoft.Extensions.Http => 0x59b67d12 => 184
	i32 1514721132, ; 229: el/Microsoft.Maui.Controls.resources.dll => 0x5a48cf6c => 300
	i32 1536373174, ; 230: System.Diagnostics.TextWriterTraceListener => 0x5b9331b6 => 30
	i32 1543031311, ; 231: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 137
	i32 1543355203, ; 232: System.Reflection.Emit.dll => 0x5bfdbb43 => 91
	i32 1550322496, ; 233: System.Reflection.Extensions.dll => 0x5c680b40 => 92
	i32 1551623176, ; 234: sk/Microsoft.Maui.Controls.resources.dll => 0x5c7be408 => 320
	i32 1565862583, ; 235: System.IO.FileSystem.Primitives => 0x5d552ab7 => 48
	i32 1566207040, ; 236: System.Threading.Tasks.Dataflow.dll => 0x5d5a6c40 => 140
	i32 1573704789, ; 237: System.Runtime.Serialization.Json => 0x5dccd455 => 111
	i32 1580037396, ; 238: System.Threading.Overlapped => 0x5e2d7514 => 139
	i32 1582372066, ; 239: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 238
	i32 1592978981, ; 240: System.Runtime.Serialization.dll => 0x5ef2ee25 => 114
	i32 1597949149, ; 241: Xamarin.Google.ErrorProne.Annotations => 0x5f3ec4dd => 286
	i32 1601112923, ; 242: System.Xml.Serialization => 0x5f6f0b5b => 156
	i32 1603525486, ; 243: Microsoft.Maui.Controls.HotReload.Forms.dll => 0x5f93db6e => 330
	i32 1604827217, ; 244: System.Net.WebClient => 0x5fa7b851 => 75
	i32 1618516317, ; 245: System.Net.WebSockets.Client.dll => 0x6078995d => 78
	i32 1622152042, ; 246: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 258
	i32 1622358360, ; 247: System.Dynamic.Runtime => 0x60b33958 => 36
	i32 1624863272, ; 248: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 280
	i32 1634654947, ; 249: CommunityToolkit.Maui.Core.dll => 0x616edae3 => 173
	i32 1635184631, ; 250: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x6176eff7 => 242
	i32 1636350590, ; 251: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 235
	i32 1639515021, ; 252: System.Net.Http.dll => 0x61b9038d => 63
	i32 1639986890, ; 253: System.Text.RegularExpressions => 0x61c036ca => 137
	i32 1641389582, ; 254: System.ComponentModel.EventBasedAsync.dll => 0x61d59e0e => 15
	i32 1653151792, ; 255: Syncfusion.Maui.Inputs.dll => 0x62891830 => 207
	i32 1657153582, ; 256: System.Runtime => 0x62c6282e => 115
	i32 1658241508, ; 257: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 274
	i32 1658251792, ; 258: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 283
	i32 1670060433, ; 259: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 230
	i32 1675553242, ; 260: System.IO.FileSystem.DriveInfo.dll => 0x63dee9da => 47
	i32 1677501392, ; 261: System.Net.Primitives.dll => 0x63fca3d0 => 69
	i32 1678508291, ; 262: System.Net.WebSockets => 0x640c0103 => 79
	i32 1679769178, ; 263: System.Security.Cryptography => 0x641f3e5a => 125
	i32 1691477237, ; 264: System.Reflection.Metadata => 0x64d1e4f5 => 93
	i32 1696967625, ; 265: System.Security.Cryptography.Csp => 0x6525abc9 => 120
	i32 1698840827, ; 266: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 290
	i32 1701541528, ; 267: System.Diagnostics.Debug.dll => 0x656b7698 => 26
	i32 1720223769, ; 268: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 0x66888819 => 251
	i32 1726116996, ; 269: System.Reflection.dll => 0x66e27484 => 96
	i32 1728033016, ; 270: System.Diagnostics.FileVersionInfo.dll => 0x66ffb0f8 => 27
	i32 1729485958, ; 271: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 226
	i32 1736233607, ; 272: ro/Microsoft.Maui.Controls.resources.dll => 0x677cd287 => 318
	i32 1743415430, ; 273: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 296
	i32 1744735666, ; 274: System.Transactions.Local.dll => 0x67fe8db2 => 148
	i32 1746316138, ; 275: Mono.Android.Export => 0x6816ab6a => 168
	i32 1750313021, ; 276: Microsoft.Win32.Primitives.dll => 0x6853a83d => 4
	i32 1758240030, ; 277: System.Resources.Reader.dll => 0x68cc9d1e => 97
	i32 1763938596, ; 278: System.Diagnostics.TraceSource.dll => 0x69239124 => 32
	i32 1765942094, ; 279: System.Reflection.Extensions => 0x6942234e => 92
	i32 1766324549, ; 280: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 273
	i32 1770582343, ; 281: Microsoft.Extensions.Logging.dll => 0x6988f147 => 185
	i32 1776026572, ; 282: System.Core.dll => 0x69dc03cc => 21
	i32 1777075843, ; 283: System.Globalization.Extensions.dll => 0x69ec0683 => 40
	i32 1780572499, ; 284: Mono.Android.Runtime.dll => 0x6a216153 => 169
	i32 1782862114, ; 285: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 312
	i32 1788241197, ; 286: Xamarin.AndroidX.Fragment => 0x6a96652d => 244
	i32 1793755602, ; 287: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 304
	i32 1808609942, ; 288: Xamarin.AndroidX.Loader => 0x6bcd3296 => 258
	i32 1813058853, ; 289: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 289
	i32 1813201214, ; 290: Xamarin.Google.Android.Material => 0x6c13413e => 283
	i32 1818569960, ; 291: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 263
	i32 1818787751, ; 292: Microsoft.VisualBasic.Core => 0x6c687fa7 => 2
	i32 1824175904, ; 293: System.Text.Encoding.Extensions => 0x6cbab720 => 133
	i32 1824722060, ; 294: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 110
	i32 1827303595, ; 295: Microsoft.VisualStudio.DesignTools.TapContract => 0x6cea70ab => 332
	i32 1828327463, ; 296: DigesettAPP => 0x6cfa1027 => 0
	i32 1828688058, ; 297: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 186
	i32 1842015223, ; 298: uk/Microsoft.Maui.Controls.resources.dll => 0x6dcaebf7 => 324
	i32 1847515442, ; 299: Xamarin.Android.Glide.Annotations => 0x6e1ed932 => 213
	i32 1853025655, ; 300: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 321
	i32 1858542181, ; 301: System.Linq.Expressions => 0x6ec71a65 => 57
	i32 1870277092, ; 302: System.Reflection.Primitives => 0x6f7a29e4 => 94
	i32 1875935024, ; 303: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 303
	i32 1879696579, ; 304: System.Formats.Tar.dll => 0x7009e4c3 => 38
	i32 1885316902, ; 305: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 224
	i32 1885918049, ; 306: Microsoft.VisualStudio.DesignTools.TapContract.dll => 0x7068d361 => 332
	i32 1888955245, ; 307: System.Diagnostics.Contracts => 0x70972b6d => 25
	i32 1889954781, ; 308: System.Reflection.Metadata.dll => 0x70a66bdd => 93
	i32 1898237753, ; 309: System.Reflection.DispatchProxy => 0x7124cf39 => 88
	i32 1900610850, ; 310: System.Resources.ResourceManager.dll => 0x71490522 => 98
	i32 1910275211, ; 311: System.Collections.NonGeneric.dll => 0x71dc7c8b => 10
	i32 1939592360, ; 312: System.Private.Xml.Linq => 0x739bd4a8 => 86
	i32 1956758971, ; 313: System.Resources.Writer => 0x74a1c5bb => 99
	i32 1961813231, ; 314: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x74eee4ef => 270
	i32 1968388702, ; 315: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 177
	i32 1983156543, ; 316: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 290
	i32 1985761444, ; 317: Xamarin.Android.Glide.GifDecoder => 0x765c50a4 => 215
	i32 1986222447, ; 318: Microsoft.IdentityModel.Tokens.dll => 0x7663596f => 194
	i32 2003115576, ; 319: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 300
	i32 2011961780, ; 320: System.Buffers.dll => 0x77ec19b4 => 7
	i32 2019465201, ; 321: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 255
	i32 2025202353, ; 322: ar/Microsoft.Maui.Controls.resources.dll => 0x78b622b1 => 295
	i32 2031763787, ; 323: Xamarin.Android.Glide => 0x791a414b => 212
	i32 2045470958, ; 324: System.Private.Xml => 0x79eb68ee => 87
	i32 2048278909, ; 325: Microsoft.Extensions.Configuration.Binder.dll => 0x7a16417d => 179
	i32 2055257422, ; 326: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 250
	i32 2060060697, ; 327: System.Windows.dll => 0x7aca0819 => 153
	i32 2066184531, ; 328: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 299
	i32 2070888862, ; 329: System.Diagnostics.TraceSource => 0x7b6f419e => 32
	i32 2079903147, ; 330: System.Runtime.dll => 0x7bf8cdab => 115
	i32 2090596640, ; 331: System.Numerics.Vectors => 0x7c9bf920 => 81
	i32 2127167465, ; 332: System.Console => 0x7ec9ffe9 => 20
	i32 2142473426, ; 333: System.Collections.Specialized => 0x7fb38cd2 => 11
	i32 2143790110, ; 334: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 161
	i32 2146852085, ; 335: Microsoft.VisualBasic.dll => 0x7ff65cf5 => 3
	i32 2159891885, ; 336: Microsoft.Maui => 0x80bd55ad => 198
	i32 2169148018, ; 337: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 307
	i32 2181898931, ; 338: Microsoft.Extensions.Options.dll => 0x820d22b3 => 188
	i32 2192057212, ; 339: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 186
	i32 2193016926, ; 340: System.ObjectModel.dll => 0x82b6c85e => 83
	i32 2201107256, ; 341: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 294
	i32 2201231467, ; 342: System.Net.Http => 0x8334206b => 63
	i32 2207618523, ; 343: it\Microsoft.Maui.Controls.resources => 0x839595db => 309
	i32 2217644978, ; 344: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 277
	i32 2222056684, ; 345: System.Threading.Tasks.Parallel => 0x8471e4ec => 142
	i32 2244775296, ; 346: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 259
	i32 2252106437, ; 347: System.Xml.Serialization.dll => 0x863c6ac5 => 156
	i32 2256313426, ; 348: System.Globalization.Extensions => 0x867c9c52 => 40
	i32 2265110946, ; 349: System.Security.AccessControl.dll => 0x8702d9a2 => 116
	i32 2266799131, ; 350: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 178
	i32 2267999099, ; 351: Xamarin.Android.Glide.DiskLruCache.dll => 0x872eeb7b => 214
	i32 2270573516, ; 352: fr/Microsoft.Maui.Controls.resources.dll => 0x875633cc => 303
	i32 2279755925, ; 353: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 266
	i32 2293034957, ; 354: System.ServiceModel.Web.dll => 0x88acefcd => 130
	i32 2295906218, ; 355: System.Net.Sockets => 0x88d8bfaa => 74
	i32 2298471582, ; 356: System.Net.Mail => 0x88ffe49e => 65
	i32 2303942373, ; 357: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 313
	i32 2305521784, ; 358: System.Private.CoreLib.dll => 0x896b7878 => 171
	i32 2315684594, ; 359: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 218
	i32 2320631194, ; 360: System.Threading.Tasks.Parallel.dll => 0x8a52059a => 142
	i32 2340441535, ; 361: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 105
	i32 2344264397, ; 362: System.ValueTuple => 0x8bbaa2cd => 150
	i32 2353062107, ; 363: System.Net.Primitives => 0x8c40e0db => 69
	i32 2354730003, ; 364: Syncfusion.Licensing => 0x8c5a5413 => 202
	i32 2368005991, ; 365: System.Xml.ReaderWriter.dll => 0x8d24e767 => 155
	i32 2369706906, ; 366: Microsoft.IdentityModel.Logging => 0x8d3edb9a => 193
	i32 2371007202, ; 367: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 177
	i32 2378619854, ; 368: System.Security.Cryptography.Csp.dll => 0x8dc6dbce => 120
	i32 2383496789, ; 369: System.Security.Principal.Windows.dll => 0x8e114655 => 126
	i32 2395872292, ; 370: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 308
	i32 2401565422, ; 371: System.Web.HttpUtility => 0x8f24faee => 151
	i32 2403452196, ; 372: Xamarin.AndroidX.Emoji2.dll => 0x8f41c524 => 241
	i32 2409983638, ; 373: Microsoft.VisualStudio.DesignTools.MobileTapContracts.dll => 0x8fa56e96 => 331
	i32 2421380589, ; 374: System.Threading.Tasks.Dataflow => 0x905355ed => 140
	i32 2423080555, ; 375: Xamarin.AndroidX.Collection.Ktx.dll => 0x906d466b => 228
	i32 2426750280, ; 376: en-US\Syncfusion.Maui.Inputs.resources => 0x90a54548 => 329
	i32 2427813419, ; 377: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 305
	i32 2435356389, ; 378: System.Console.dll => 0x912896e5 => 20
	i32 2435904999, ; 379: System.ComponentModel.DataAnnotations.dll => 0x9130f5e7 => 14
	i32 2454642406, ; 380: System.Text.Encoding.dll => 0x924edee6 => 134
	i32 2458678730, ; 381: System.Net.Sockets.dll => 0x928c75ca => 74
	i32 2459001652, ; 382: System.Linq.Parallel.dll => 0x92916334 => 58
	i32 2465532216, ; 383: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 231
	i32 2471841756, ; 384: netstandard.dll => 0x93554fdc => 166
	i32 2475788418, ; 385: Java.Interop.dll => 0x93918882 => 167
	i32 2480646305, ; 386: Microsoft.Maui.Controls => 0x93dba8a1 => 196
	i32 2483903535, ; 387: System.ComponentModel.EventBasedAsync => 0x940d5c2f => 15
	i32 2484371297, ; 388: System.Net.ServicePoint => 0x94147f61 => 73
	i32 2490993605, ; 389: System.AppContext.dll => 0x94798bc5 => 6
	i32 2501346920, ; 390: System.Data.DataSetExtensions => 0x95178668 => 23
	i32 2505896520, ; 391: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 253
	i32 2522472828, ; 392: Xamarin.Android.Glide.dll => 0x9659e17c => 212
	i32 2538310050, ; 393: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 90
	i32 2550873716, ; 394: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 306
	i32 2562349572, ; 395: Microsoft.CSharp => 0x98ba5a04 => 1
	i32 2570120770, ; 396: System.Text.Encodings.Web => 0x9930ee42 => 135
	i32 2581783588, ; 397: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 0x99e2e424 => 254
	i32 2581819634, ; 398: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 276
	i32 2585220780, ; 399: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 133
	i32 2585805581, ; 400: System.Net.Ping => 0x9a20430d => 68
	i32 2589602615, ; 401: System.Threading.ThreadPool => 0x9a5a3337 => 145
	i32 2593496499, ; 402: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 315
	i32 2605712449, ; 403: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 294
	i32 2615233544, ; 404: Xamarin.AndroidX.Fragment.Ktx => 0x9be14c08 => 245
	i32 2616218305, ; 405: Microsoft.Extensions.Logging.Debug.dll => 0x9bf052c1 => 187
	i32 2617129537, ; 406: System.Private.Xml.dll => 0x9bfe3a41 => 87
	i32 2618712057, ; 407: System.Reflection.TypeExtensions.dll => 0x9c165ff9 => 95
	i32 2620871830, ; 408: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 235
	i32 2624644809, ; 409: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 240
	i32 2626831493, ; 410: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 310
	i32 2627185994, ; 411: System.Diagnostics.TextWriterTraceListener.dll => 0x9c97ad4a => 30
	i32 2629843544, ; 412: System.IO.Compression.ZipFile.dll => 0x9cc03a58 => 44
	i32 2633051222, ; 413: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 249
	i32 2640290731, ; 414: Microsoft.IdentityModel.Logging.dll => 0x9d5fa3ab => 193
	i32 2663391936, ; 415: Xamarin.Android.Glide.DiskLruCache => 0x9ec022c0 => 214
	i32 2663698177, ; 416: System.Runtime.Loader => 0x9ec4cf01 => 108
	i32 2664396074, ; 417: System.Xml.XDocument.dll => 0x9ecf752a => 157
	i32 2665622720, ; 418: System.Drawing.Primitives => 0x9ee22cc0 => 34
	i32 2676780864, ; 419: System.Data.Common.dll => 0x9f8c6f40 => 22
	i32 2686887180, ; 420: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 113
	i32 2693849962, ; 421: System.IO.dll => 0xa090e36a => 56
	i32 2701096212, ; 422: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 274
	i32 2715334215, ; 423: System.Threading.Tasks.dll => 0xa1d8b647 => 143
	i32 2717744543, ; 424: System.Security.Claims => 0xa1fd7d9f => 117
	i32 2719963679, ; 425: System.Security.Cryptography.Cng.dll => 0xa21f5a1f => 119
	i32 2724373263, ; 426: System.Runtime.Numerics.dll => 0xa262a30f => 109
	i32 2732626843, ; 427: Xamarin.AndroidX.Activity => 0xa2e0939b => 216
	i32 2735172069, ; 428: System.Threading.Channels => 0xa30769e5 => 138
	i32 2737747696, ; 429: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 222
	i32 2740948882, ; 430: System.IO.Pipes.AccessControl => 0xa35f8f92 => 53
	i32 2743368605, ; 431: Syncfusion.Maui.DataSource => 0xa3847b9d => 204
	i32 2748088231, ; 432: System.Runtime.InteropServices.JavaScript => 0xa3cc7fa7 => 104
	i32 2752995522, ; 433: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 316
	i32 2758225723, ; 434: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 197
	i32 2764765095, ; 435: Microsoft.Maui.dll => 0xa4caf7a7 => 198
	i32 2765824710, ; 436: System.Text.Encoding.CodePages.dll => 0xa4db22c6 => 132
	i32 2770495804, ; 437: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 288
	i32 2778768386, ; 438: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 279
	i32 2779977773, ; 439: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0xa5b3182d => 267
	i32 2785988530, ; 440: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 322
	i32 2788224221, ; 441: Xamarin.AndroidX.Fragment.Ktx.dll => 0xa630ecdd => 245
	i32 2801831435, ; 442: Microsoft.Maui.Graphics => 0xa7008e0b => 200
	i32 2803228030, ; 443: System.Xml.XPath.XDocument.dll => 0xa715dd7e => 158
	i32 2806116107, ; 444: es/Microsoft.Maui.Controls.resources.dll => 0xa741ef0b => 301
	i32 2810250172, ; 445: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 232
	i32 2819470561, ; 446: System.Xml.dll => 0xa80db4e1 => 162
	i32 2821205001, ; 447: System.ServiceProcess.dll => 0xa8282c09 => 131
	i32 2821294376, ; 448: Xamarin.AndroidX.ResourceInspection.Annotation => 0xa8298928 => 267
	i32 2824502124, ; 449: System.Xml.XmlDocument => 0xa85a7b6c => 160
	i32 2831556043, ; 450: nl/Microsoft.Maui.Controls.resources.dll => 0xa8c61dcb => 314
	i32 2838993487, ; 451: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 0xa9379a4f => 256
	i32 2849599387, ; 452: System.Threading.Overlapped.dll => 0xa9d96f9b => 139
	i32 2853208004, ; 453: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 279
	i32 2855708567, ; 454: Xamarin.AndroidX.Transition => 0xaa36a797 => 275
	i32 2861098320, ; 455: Mono.Android.Export.dll => 0xaa88e550 => 168
	i32 2861189240, ; 456: Microsoft.Maui.Essentials => 0xaa8a4878 => 199
	i32 2868488919, ; 457: CommunityToolkit.Maui.Core => 0xaaf9aad7 => 173
	i32 2868557005, ; 458: Syncfusion.Licensing.dll => 0xaafab4cd => 202
	i32 2870099610, ; 459: Xamarin.AndroidX.Activity.Ktx.dll => 0xab123e9a => 217
	i32 2875164099, ; 460: Jsr305Binding.dll => 0xab5f85c3 => 284
	i32 2875220617, ; 461: System.Globalization.Calendars.dll => 0xab606289 => 39
	i32 2884993177, ; 462: Xamarin.AndroidX.ExifInterface => 0xabf58099 => 243
	i32 2887636118, ; 463: System.Net.dll => 0xac1dd496 => 80
	i32 2899753641, ; 464: System.IO.UnmanagedMemoryStream => 0xacd6baa9 => 55
	i32 2900621748, ; 465: System.Dynamic.Runtime.dll => 0xace3f9b4 => 36
	i32 2901442782, ; 466: System.Reflection => 0xacf080de => 96
	i32 2905242038, ; 467: mscorlib.dll => 0xad2a79b6 => 165
	i32 2909740682, ; 468: System.Private.CoreLib => 0xad6f1e8a => 171
	i32 2916838712, ; 469: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 280
	i32 2919462931, ; 470: System.Numerics.Vectors.dll => 0xae037813 => 81
	i32 2921128767, ; 471: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 219
	i32 2936416060, ; 472: System.Resources.Reader => 0xaf06273c => 97
	i32 2940926066, ; 473: System.Diagnostics.StackTrace.dll => 0xaf4af872 => 29
	i32 2942453041, ; 474: System.Xml.XPath.XDocument => 0xaf624531 => 158
	i32 2959614098, ; 475: System.ComponentModel.dll => 0xb0682092 => 18
	i32 2968338931, ; 476: System.Security.Principal.Windows => 0xb0ed41f3 => 126
	i32 2971004615, ; 477: Microsoft.Extensions.Options.ConfigurationExtensions.dll => 0xb115eec7 => 189
	i32 2972252294, ; 478: System.Security.Cryptography.Algorithms.dll => 0xb128f886 => 118
	i32 2978675010, ; 479: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 239
	i32 2987532451, ; 480: Xamarin.AndroidX.Security.SecurityCrypto => 0xb21220a3 => 270
	i32 2996846495, ; 481: Xamarin.AndroidX.Lifecycle.Process.dll => 0xb2a03f9f => 252
	i32 3016983068, ; 482: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 272
	i32 3020703001, ; 483: Microsoft.Extensions.Diagnostics => 0xb40c4519 => 182
	i32 3023353419, ; 484: WindowsBase.dll => 0xb434b64b => 164
	i32 3024354802, ; 485: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 247
	i32 3038032645, ; 486: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 333
	i32 3056245963, ; 487: Xamarin.AndroidX.SavedState.SavedState.Ktx => 0xb62a9ccb => 269
	i32 3057625584, ; 488: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 260
	i32 3059408633, ; 489: Mono.Android.Runtime => 0xb65adef9 => 169
	i32 3059793426, ; 490: System.ComponentModel.Primitives => 0xb660be12 => 16
	i32 3075834255, ; 491: System.Threading.Tasks => 0xb755818f => 143
	i32 3077302341, ; 492: hu/Microsoft.Maui.Controls.resources.dll => 0xb76be845 => 307
	i32 3084678329, ; 493: Microsoft.IdentityModel.Tokens => 0xb7dc74b9 => 194
	i32 3090735792, ; 494: System.Security.Cryptography.X509Certificates.dll => 0xb838e2b0 => 124
	i32 3099732863, ; 495: System.Security.Claims.dll => 0xb8c22b7f => 117
	i32 3103600923, ; 496: System.Formats.Asn1 => 0xb8fd311b => 37
	i32 3111772706, ; 497: System.Runtime.Serialization => 0xb979e222 => 114
	i32 3121463068, ; 498: System.IO.FileSystem.AccessControl.dll => 0xba0dbf1c => 46
	i32 3124832203, ; 499: System.Threading.Tasks.Extensions => 0xba4127cb => 141
	i32 3132293585, ; 500: System.Security.AccessControl => 0xbab301d1 => 116
	i32 3140633799, ; 501: Syncfusion.Maui.ListView => 0xbb3244c7 => 208
	i32 3147165239, ; 502: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 33
	i32 3147228406, ; 503: Syncfusion.Maui.Core => 0xbb96e4f6 => 203
	i32 3148237826, ; 504: GoogleGson.dll => 0xbba64c02 => 175
	i32 3159123045, ; 505: System.Reflection.Primitives.dll => 0xbc4c6465 => 94
	i32 3160747431, ; 506: System.IO.MemoryMappedFiles => 0xbc652da7 => 52
	i32 3170039328, ; 507: Syncfusion.Maui.ListView.dll => 0xbcf2f620 => 208
	i32 3178803400, ; 508: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 261
	i32 3192346100, ; 509: System.Security.SecureString => 0xbe4755f4 => 128
	i32 3193515020, ; 510: System.Web => 0xbe592c0c => 152
	i32 3204380047, ; 511: System.Data.dll => 0xbefef58f => 24
	i32 3209718065, ; 512: System.Xml.XmlDocument.dll => 0xbf506931 => 160
	i32 3211777861, ; 513: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 238
	i32 3220365878, ; 514: System.Threading => 0xbff2e236 => 147
	i32 3226221578, ; 515: System.Runtime.Handles.dll => 0xc04c3c0a => 103
	i32 3251039220, ; 516: System.Reflection.DispatchProxy.dll => 0xc1c6ebf4 => 88
	i32 3258312781, ; 517: Xamarin.AndroidX.CardView => 0xc235e84d => 226
	i32 3265493905, ; 518: System.Linq.Queryable.dll => 0xc2a37b91 => 59
	i32 3265893370, ; 519: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 141
	i32 3277815716, ; 520: System.Resources.Writer.dll => 0xc35f7fa4 => 99
	i32 3279906254, ; 521: Microsoft.Win32.Registry.dll => 0xc37f65ce => 5
	i32 3280506390, ; 522: System.ComponentModel.Annotations.dll => 0xc3888e16 => 13
	i32 3290767353, ; 523: System.Security.Cryptography.Encoding => 0xc4251ff9 => 121
	i32 3299363146, ; 524: System.Text.Encoding => 0xc4a8494a => 134
	i32 3303498502, ; 525: System.Diagnostics.FileVersionInfo => 0xc4e76306 => 27
	i32 3305363605, ; 526: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 302
	i32 3306970809, ; 527: Syncfusion.Maui.Inputs => 0xc51c5eb9 => 207
	i32 3312457198, ; 528: Microsoft.IdentityModel.JsonWebTokens => 0xc57015ee => 192
	i32 3316684772, ; 529: System.Net.Requests.dll => 0xc5b097e4 => 71
	i32 3317135071, ; 530: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 236
	i32 3317144872, ; 531: System.Data => 0xc5b79d28 => 24
	i32 3340431453, ; 532: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 224
	i32 3345895724, ; 533: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 265
	i32 3346324047, ; 534: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 262
	i32 3357674450, ; 535: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 319
	i32 3358260929, ; 536: System.Text.Json => 0xc82afec1 => 136
	i32 3362336904, ; 537: Xamarin.AndroidX.Activity.Ktx => 0xc8693088 => 217
	i32 3362522851, ; 538: Xamarin.AndroidX.Core => 0xc86c06e3 => 233
	i32 3366347497, ; 539: Java.Interop => 0xc8a662e9 => 167
	i32 3374999561, ; 540: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 266
	i32 3381016424, ; 541: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 298
	i32 3381934622, ; 542: Syncfusion.Maui.GridCommon => 0xc9943a1e => 206
	i32 3395150330, ; 543: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 100
	i32 3403906625, ; 544: System.Security.Cryptography.OpenSsl.dll => 0xcae37e41 => 122
	i32 3405233483, ; 545: Xamarin.AndroidX.CustomView.PoolingContainer => 0xcaf7bd4b => 237
	i32 3421170118, ; 546: Microsoft.Extensions.Configuration.Binder => 0xcbeae9c6 => 179
	i32 3428513518, ; 547: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 180
	i32 3429136800, ; 548: System.Xml => 0xcc6479a0 => 162
	i32 3430777524, ; 549: netstandard => 0xcc7d82b4 => 166
	i32 3441283291, ; 550: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 240
	i32 3445260447, ; 551: System.Formats.Tar => 0xcd5a809f => 38
	i32 3452344032, ; 552: Microsoft.Maui.Controls.Compatibility.dll => 0xcdc696e0 => 195
	i32 3463511458, ; 553: hr/Microsoft.Maui.Controls.resources.dll => 0xce70fda2 => 306
	i32 3471940407, ; 554: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 17
	i32 3476120550, ; 555: Mono.Android => 0xcf3163e6 => 170
	i32 3479583265, ; 556: ru/Microsoft.Maui.Controls.resources.dll => 0xcf663a21 => 319
	i32 3484440000, ; 557: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 318
	i32 3485117614, ; 558: System.Text.Json.dll => 0xcfbaacae => 136
	i32 3486566296, ; 559: System.Transactions => 0xcfd0c798 => 149
	i32 3493954962, ; 560: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 229
	i32 3509114376, ; 561: System.Xml.Linq => 0xd128d608 => 154
	i32 3515174580, ; 562: System.Security.dll => 0xd1854eb4 => 129
	i32 3530912306, ; 563: System.Configuration => 0xd2757232 => 19
	i32 3539954161, ; 564: System.Net.HttpListener => 0xd2ff69f1 => 64
	i32 3558305335, ; 565: Syncfusion.Maui.DataSource.dll => 0xd4176e37 => 204
	i32 3560100363, ; 566: System.Threading.Timer => 0xd432d20b => 146
	i32 3570554715, ; 567: System.IO.FileSystem.AccessControl => 0xd4d2575b => 46
	i32 3580758918, ; 568: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 326
	i32 3597029428, ; 569: Xamarin.Android.Glide.GifDecoder.dll => 0xd6665034 => 215
	i32 3598340787, ; 570: System.Net.WebSockets.Client => 0xd67a52b3 => 78
	i32 3608519521, ; 571: System.Linq.dll => 0xd715a361 => 60
	i32 3624195450, ; 572: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 105
	i32 3627220390, ; 573: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 264
	i32 3633644679, ; 574: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 219
	i32 3638274909, ; 575: System.IO.FileSystem.Primitives.dll => 0xd8dbab5d => 48
	i32 3641597786, ; 576: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 250
	i32 3643446276, ; 577: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 323
	i32 3643854240, ; 578: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 261
	i32 3645089577, ; 579: System.ComponentModel.DataAnnotations => 0xd943a729 => 14
	i32 3657292374, ; 580: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 178
	i32 3660523487, ; 581: System.Net.NetworkInformation => 0xda2f27df => 67
	i32 3672681054, ; 582: Mono.Android.dll => 0xdae8aa5e => 170
	i32 3676670898, ; 583: Microsoft.Maui.Controls.HotReload.Forms => 0xdb258bb2 => 330
	i32 3682565725, ; 584: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 225
	i32 3684561358, ; 585: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 229
	i32 3697841164, ; 586: zh-Hant/Microsoft.Maui.Controls.resources.dll => 0xdc68940c => 328
	i32 3700591436, ; 587: Microsoft.IdentityModel.Abstractions.dll => 0xdc928b4c => 191
	i32 3700866549, ; 588: System.Net.WebProxy.dll => 0xdc96bdf5 => 77
	i32 3706696989, ; 589: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 234
	i32 3716563718, ; 590: System.Runtime.Intrinsics => 0xdd864306 => 107
	i32 3718780102, ; 591: Xamarin.AndroidX.Annotation => 0xdda814c6 => 218
	i32 3724971120, ; 592: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 260
	i32 3732100267, ; 593: System.Net.NameResolution => 0xde7354ab => 66
	i32 3737834244, ; 594: System.Net.Http.Json.dll => 0xdecad304 => 62
	i32 3748608112, ; 595: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 210
	i32 3751444290, ; 596: System.Xml.XPath => 0xdf9a7f42 => 159
	i32 3786282454, ; 597: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 227
	i32 3792276235, ; 598: System.Collections.NonGeneric => 0xe2098b0b => 10
	i32 3800979733, ; 599: Microsoft.Maui.Controls.Compatibility => 0xe28e5915 => 195
	i32 3802395368, ; 600: System.Collections.Specialized.dll => 0xe2a3f2e8 => 11
	i32 3817368567, ; 601: CommunityToolkit.Maui.dll => 0xe3886bf7 => 172
	i32 3819260425, ; 602: System.Net.WebProxy => 0xe3a54a09 => 77
	i32 3823082795, ; 603: System.Security.Cryptography.dll => 0xe3df9d2b => 125
	i32 3829621856, ; 604: System.Numerics.dll => 0xe4436460 => 82
	i32 3841636137, ; 605: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 181
	i32 3844307129, ; 606: System.Net.Mail.dll => 0xe52378b9 => 65
	i32 3849253459, ; 607: System.Runtime.InteropServices.dll => 0xe56ef253 => 106
	i32 3870376305, ; 608: System.Net.HttpListener.dll => 0xe6b14171 => 64
	i32 3873536506, ; 609: System.Security.Principal => 0xe6e179fa => 127
	i32 3875112723, ; 610: System.Security.Cryptography.Encoding.dll => 0xe6f98713 => 121
	i32 3885497537, ; 611: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 76
	i32 3885922214, ; 612: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 275
	i32 3888767677, ; 613: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 265
	i32 3889960447, ; 614: zh-Hans/Microsoft.Maui.Controls.resources.dll => 0xe7dc15ff => 327
	i32 3896106733, ; 615: System.Collections.Concurrent.dll => 0xe839deed => 8
	i32 3896760992, ; 616: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 233
	i32 3901907137, ; 617: Microsoft.VisualBasic.Core.dll => 0xe89260c1 => 2
	i32 3920810846, ; 618: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 43
	i32 3921031405, ; 619: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 278
	i32 3928044579, ; 620: System.Xml.ReaderWriter => 0xea213423 => 155
	i32 3930554604, ; 621: System.Security.Principal.dll => 0xea4780ec => 127
	i32 3931092270, ; 622: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 263
	i32 3945713374, ; 623: System.Data.DataSetExtensions.dll => 0xeb2ecede => 23
	i32 3953953790, ; 624: System.Text.Encoding.CodePages => 0xebac8bfe => 132
	i32 3955647286, ; 625: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 221
	i32 3959773229, ; 626: Xamarin.AndroidX.Lifecycle.Process => 0xec05582d => 252
	i32 3980434154, ; 627: th/Microsoft.Maui.Controls.resources.dll => 0xed409aea => 322
	i32 3982571493, ; 628: en-US/Syncfusion.Maui.Inputs.resources.dll => 0xed6137e5 => 329
	i32 3987592930, ; 629: he/Microsoft.Maui.Controls.resources.dll => 0xedadd6e2 => 304
	i32 4003436829, ; 630: System.Diagnostics.Process.dll => 0xee9f991d => 28
	i32 4015948917, ; 631: Xamarin.AndroidX.Annotation.Jvm.dll => 0xef5e8475 => 220
	i32 4019855520, ; 632: DigesettAPP.dll => 0xef9a20a0 => 0
	i32 4025784931, ; 633: System.Memory => 0xeff49a63 => 61
	i32 4046471985, ; 634: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 197
	i32 4054681211, ; 635: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 89
	i32 4068434129, ; 636: System.Private.Xml.Linq.dll => 0xf27f60d1 => 86
	i32 4073602200, ; 637: System.Threading.dll => 0xf2ce3c98 => 147
	i32 4094352644, ; 638: Microsoft.Maui.Essentials.dll => 0xf40add04 => 199
	i32 4099507663, ; 639: System.Drawing.dll => 0xf45985cf => 35
	i32 4100113165, ; 640: System.Private.Uri => 0xf462c30d => 85
	i32 4101593132, ; 641: Xamarin.AndroidX.Emoji2 => 0xf479582c => 241
	i32 4102112229, ; 642: pt/Microsoft.Maui.Controls.resources.dll => 0xf48143e5 => 317
	i32 4125707920, ; 643: ms/Microsoft.Maui.Controls.resources.dll => 0xf5e94e90 => 312
	i32 4126470640, ; 644: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 180
	i32 4127667938, ; 645: System.IO.FileSystem.Watcher => 0xf60736e2 => 49
	i32 4130442656, ; 646: System.AppContext => 0xf6318da0 => 6
	i32 4147896353, ; 647: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 89
	i32 4150914736, ; 648: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 324
	i32 4151237749, ; 649: System.Core => 0xf76edc75 => 21
	i32 4159265925, ; 650: System.Xml.XmlSerializer => 0xf7e95c85 => 161
	i32 4161255271, ; 651: System.Reflection.TypeExtensions => 0xf807b767 => 95
	i32 4164802419, ; 652: System.IO.FileSystem.Watcher.dll => 0xf83dd773 => 49
	i32 4181436372, ; 653: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 112
	i32 4182413190, ; 654: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 257
	i32 4182880526, ; 655: Microsoft.VisualStudio.DesignTools.MobileTapContracts => 0xf951b10e => 331
	i32 4185676441, ; 656: System.Security => 0xf97c5a99 => 129
	i32 4196529839, ; 657: System.Net.WebClient.dll => 0xfa21f6af => 75
	i32 4213026141, ; 658: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 210
	i32 4256097574, ; 659: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 234
	i32 4258378803, ; 660: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 0xfdd1b433 => 256
	i32 4260525087, ; 661: System.Buffers => 0xfdf2741f => 7
	i32 4263231520, ; 662: System.IdentityModel.Tokens.Jwt.dll => 0xfe1bc020 => 211
	i32 4271975918, ; 663: Microsoft.Maui.Controls.dll => 0xfea12dee => 196
	i32 4274623895, ; 664: CommunityToolkit.Mvvm.dll => 0xfec99597 => 174
	i32 4274976490, ; 665: System.Runtime.Numerics => 0xfecef6ea => 109
	i32 4292120959, ; 666: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 257
	i32 4294763496 ; 667: Xamarin.AndroidX.ExifInterface.dll => 0xfffce3e8 => 243
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [668 x i32] [
	i32 67, ; 0
	i32 66, ; 1
	i32 107, ; 2
	i32 253, ; 3
	i32 287, ; 4
	i32 47, ; 5
	i32 201, ; 6
	i32 79, ; 7
	i32 144, ; 8
	i32 29, ; 9
	i32 328, ; 10
	i32 123, ; 11
	i32 200, ; 12
	i32 101, ; 13
	i32 183, ; 14
	i32 271, ; 15
	i32 106, ; 16
	i32 271, ; 17
	i32 138, ; 18
	i32 291, ; 19
	i32 76, ; 20
	i32 123, ; 21
	i32 13, ; 22
	i32 227, ; 23
	i32 203, ; 24
	i32 131, ; 25
	i32 205, ; 26
	i32 273, ; 27
	i32 150, ; 28
	i32 325, ; 29
	i32 326, ; 30
	i32 18, ; 31
	i32 225, ; 32
	i32 26, ; 33
	i32 182, ; 34
	i32 247, ; 35
	i32 1, ; 36
	i32 58, ; 37
	i32 41, ; 38
	i32 90, ; 39
	i32 230, ; 40
	i32 146, ; 41
	i32 249, ; 42
	i32 246, ; 43
	i32 297, ; 44
	i32 53, ; 45
	i32 184, ; 46
	i32 68, ; 47
	i32 205, ; 48
	i32 325, ; 49
	i32 216, ; 50
	i32 82, ; 51
	i32 310, ; 52
	i32 248, ; 53
	i32 309, ; 54
	i32 130, ; 55
	i32 54, ; 56
	i32 209, ; 57
	i32 148, ; 58
	i32 73, ; 59
	i32 144, ; 60
	i32 176, ; 61
	i32 61, ; 62
	i32 145, ; 63
	i32 333, ; 64
	i32 164, ; 65
	i32 321, ; 66
	i32 231, ; 67
	i32 12, ; 68
	i32 244, ; 69
	i32 124, ; 70
	i32 151, ; 71
	i32 206, ; 72
	i32 112, ; 73
	i32 165, ; 74
	i32 163, ; 75
	i32 246, ; 76
	i32 191, ; 77
	i32 259, ; 78
	i32 83, ; 79
	i32 308, ; 80
	i32 302, ; 81
	i32 190, ; 82
	i32 149, ; 83
	i32 291, ; 84
	i32 59, ; 85
	i32 185, ; 86
	i32 50, ; 87
	i32 102, ; 88
	i32 113, ; 89
	i32 39, ; 90
	i32 284, ; 91
	i32 282, ; 92
	i32 119, ; 93
	i32 316, ; 94
	i32 172, ; 95
	i32 51, ; 96
	i32 43, ; 97
	i32 118, ; 98
	i32 236, ; 99
	i32 314, ; 100
	i32 242, ; 101
	i32 80, ; 102
	i32 135, ; 103
	i32 278, ; 104
	i32 223, ; 105
	i32 8, ; 106
	i32 72, ; 107
	i32 296, ; 108
	i32 154, ; 109
	i32 293, ; 110
	i32 153, ; 111
	i32 209, ; 112
	i32 91, ; 113
	i32 288, ; 114
	i32 44, ; 115
	i32 311, ; 116
	i32 299, ; 117
	i32 292, ; 118
	i32 108, ; 119
	i32 189, ; 120
	i32 128, ; 121
	i32 25, ; 122
	i32 213, ; 123
	i32 71, ; 124
	i32 54, ; 125
	i32 45, ; 126
	i32 320, ; 127
	i32 188, ; 128
	i32 237, ; 129
	i32 22, ; 130
	i32 251, ; 131
	i32 85, ; 132
	i32 42, ; 133
	i32 159, ; 134
	i32 70, ; 135
	i32 264, ; 136
	i32 3, ; 137
	i32 41, ; 138
	i32 62, ; 139
	i32 16, ; 140
	i32 52, ; 141
	i32 323, ; 142
	i32 287, ; 143
	i32 104, ; 144
	i32 201, ; 145
	i32 292, ; 146
	i32 285, ; 147
	i32 248, ; 148
	i32 33, ; 149
	i32 157, ; 150
	i32 84, ; 151
	i32 31, ; 152
	i32 12, ; 153
	i32 50, ; 154
	i32 55, ; 155
	i32 268, ; 156
	i32 35, ; 157
	i32 181, ; 158
	i32 298, ; 159
	i32 286, ; 160
	i32 221, ; 161
	i32 34, ; 162
	i32 57, ; 163
	i32 183, ; 164
	i32 255, ; 165
	i32 175, ; 166
	i32 17, ; 167
	i32 289, ; 168
	i32 163, ; 169
	i32 311, ; 170
	i32 254, ; 171
	i32 187, ; 172
	i32 281, ; 173
	i32 176, ; 174
	i32 317, ; 175
	i32 152, ; 176
	i32 277, ; 177
	i32 262, ; 178
	i32 315, ; 179
	i32 223, ; 180
	i32 28, ; 181
	i32 174, ; 182
	i32 51, ; 183
	i32 313, ; 184
	i32 282, ; 185
	i32 5, ; 186
	i32 297, ; 187
	i32 272, ; 188
	i32 276, ; 189
	i32 228, ; 190
	i32 293, ; 191
	i32 220, ; 192
	i32 239, ; 193
	i32 84, ; 194
	i32 281, ; 195
	i32 60, ; 196
	i32 111, ; 197
	i32 56, ; 198
	i32 327, ; 199
	i32 268, ; 200
	i32 98, ; 201
	i32 19, ; 202
	i32 232, ; 203
	i32 110, ; 204
	i32 100, ; 205
	i32 101, ; 206
	i32 295, ; 207
	i32 103, ; 208
	i32 285, ; 209
	i32 70, ; 210
	i32 37, ; 211
	i32 31, ; 212
	i32 102, ; 213
	i32 72, ; 214
	i32 211, ; 215
	i32 301, ; 216
	i32 9, ; 217
	i32 122, ; 218
	i32 45, ; 219
	i32 222, ; 220
	i32 190, ; 221
	i32 9, ; 222
	i32 42, ; 223
	i32 4, ; 224
	i32 269, ; 225
	i32 305, ; 226
	i32 192, ; 227
	i32 184, ; 228
	i32 300, ; 229
	i32 30, ; 230
	i32 137, ; 231
	i32 91, ; 232
	i32 92, ; 233
	i32 320, ; 234
	i32 48, ; 235
	i32 140, ; 236
	i32 111, ; 237
	i32 139, ; 238
	i32 238, ; 239
	i32 114, ; 240
	i32 286, ; 241
	i32 156, ; 242
	i32 330, ; 243
	i32 75, ; 244
	i32 78, ; 245
	i32 258, ; 246
	i32 36, ; 247
	i32 280, ; 248
	i32 173, ; 249
	i32 242, ; 250
	i32 235, ; 251
	i32 63, ; 252
	i32 137, ; 253
	i32 15, ; 254
	i32 207, ; 255
	i32 115, ; 256
	i32 274, ; 257
	i32 283, ; 258
	i32 230, ; 259
	i32 47, ; 260
	i32 69, ; 261
	i32 79, ; 262
	i32 125, ; 263
	i32 93, ; 264
	i32 120, ; 265
	i32 290, ; 266
	i32 26, ; 267
	i32 251, ; 268
	i32 96, ; 269
	i32 27, ; 270
	i32 226, ; 271
	i32 318, ; 272
	i32 296, ; 273
	i32 148, ; 274
	i32 168, ; 275
	i32 4, ; 276
	i32 97, ; 277
	i32 32, ; 278
	i32 92, ; 279
	i32 273, ; 280
	i32 185, ; 281
	i32 21, ; 282
	i32 40, ; 283
	i32 169, ; 284
	i32 312, ; 285
	i32 244, ; 286
	i32 304, ; 287
	i32 258, ; 288
	i32 289, ; 289
	i32 283, ; 290
	i32 263, ; 291
	i32 2, ; 292
	i32 133, ; 293
	i32 110, ; 294
	i32 332, ; 295
	i32 0, ; 296
	i32 186, ; 297
	i32 324, ; 298
	i32 213, ; 299
	i32 321, ; 300
	i32 57, ; 301
	i32 94, ; 302
	i32 303, ; 303
	i32 38, ; 304
	i32 224, ; 305
	i32 332, ; 306
	i32 25, ; 307
	i32 93, ; 308
	i32 88, ; 309
	i32 98, ; 310
	i32 10, ; 311
	i32 86, ; 312
	i32 99, ; 313
	i32 270, ; 314
	i32 177, ; 315
	i32 290, ; 316
	i32 215, ; 317
	i32 194, ; 318
	i32 300, ; 319
	i32 7, ; 320
	i32 255, ; 321
	i32 295, ; 322
	i32 212, ; 323
	i32 87, ; 324
	i32 179, ; 325
	i32 250, ; 326
	i32 153, ; 327
	i32 299, ; 328
	i32 32, ; 329
	i32 115, ; 330
	i32 81, ; 331
	i32 20, ; 332
	i32 11, ; 333
	i32 161, ; 334
	i32 3, ; 335
	i32 198, ; 336
	i32 307, ; 337
	i32 188, ; 338
	i32 186, ; 339
	i32 83, ; 340
	i32 294, ; 341
	i32 63, ; 342
	i32 309, ; 343
	i32 277, ; 344
	i32 142, ; 345
	i32 259, ; 346
	i32 156, ; 347
	i32 40, ; 348
	i32 116, ; 349
	i32 178, ; 350
	i32 214, ; 351
	i32 303, ; 352
	i32 266, ; 353
	i32 130, ; 354
	i32 74, ; 355
	i32 65, ; 356
	i32 313, ; 357
	i32 171, ; 358
	i32 218, ; 359
	i32 142, ; 360
	i32 105, ; 361
	i32 150, ; 362
	i32 69, ; 363
	i32 202, ; 364
	i32 155, ; 365
	i32 193, ; 366
	i32 177, ; 367
	i32 120, ; 368
	i32 126, ; 369
	i32 308, ; 370
	i32 151, ; 371
	i32 241, ; 372
	i32 331, ; 373
	i32 140, ; 374
	i32 228, ; 375
	i32 329, ; 376
	i32 305, ; 377
	i32 20, ; 378
	i32 14, ; 379
	i32 134, ; 380
	i32 74, ; 381
	i32 58, ; 382
	i32 231, ; 383
	i32 166, ; 384
	i32 167, ; 385
	i32 196, ; 386
	i32 15, ; 387
	i32 73, ; 388
	i32 6, ; 389
	i32 23, ; 390
	i32 253, ; 391
	i32 212, ; 392
	i32 90, ; 393
	i32 306, ; 394
	i32 1, ; 395
	i32 135, ; 396
	i32 254, ; 397
	i32 276, ; 398
	i32 133, ; 399
	i32 68, ; 400
	i32 145, ; 401
	i32 315, ; 402
	i32 294, ; 403
	i32 245, ; 404
	i32 187, ; 405
	i32 87, ; 406
	i32 95, ; 407
	i32 235, ; 408
	i32 240, ; 409
	i32 310, ; 410
	i32 30, ; 411
	i32 44, ; 412
	i32 249, ; 413
	i32 193, ; 414
	i32 214, ; 415
	i32 108, ; 416
	i32 157, ; 417
	i32 34, ; 418
	i32 22, ; 419
	i32 113, ; 420
	i32 56, ; 421
	i32 274, ; 422
	i32 143, ; 423
	i32 117, ; 424
	i32 119, ; 425
	i32 109, ; 426
	i32 216, ; 427
	i32 138, ; 428
	i32 222, ; 429
	i32 53, ; 430
	i32 204, ; 431
	i32 104, ; 432
	i32 316, ; 433
	i32 197, ; 434
	i32 198, ; 435
	i32 132, ; 436
	i32 288, ; 437
	i32 279, ; 438
	i32 267, ; 439
	i32 322, ; 440
	i32 245, ; 441
	i32 200, ; 442
	i32 158, ; 443
	i32 301, ; 444
	i32 232, ; 445
	i32 162, ; 446
	i32 131, ; 447
	i32 267, ; 448
	i32 160, ; 449
	i32 314, ; 450
	i32 256, ; 451
	i32 139, ; 452
	i32 279, ; 453
	i32 275, ; 454
	i32 168, ; 455
	i32 199, ; 456
	i32 173, ; 457
	i32 202, ; 458
	i32 217, ; 459
	i32 284, ; 460
	i32 39, ; 461
	i32 243, ; 462
	i32 80, ; 463
	i32 55, ; 464
	i32 36, ; 465
	i32 96, ; 466
	i32 165, ; 467
	i32 171, ; 468
	i32 280, ; 469
	i32 81, ; 470
	i32 219, ; 471
	i32 97, ; 472
	i32 29, ; 473
	i32 158, ; 474
	i32 18, ; 475
	i32 126, ; 476
	i32 189, ; 477
	i32 118, ; 478
	i32 239, ; 479
	i32 270, ; 480
	i32 252, ; 481
	i32 272, ; 482
	i32 182, ; 483
	i32 164, ; 484
	i32 247, ; 485
	i32 333, ; 486
	i32 269, ; 487
	i32 260, ; 488
	i32 169, ; 489
	i32 16, ; 490
	i32 143, ; 491
	i32 307, ; 492
	i32 194, ; 493
	i32 124, ; 494
	i32 117, ; 495
	i32 37, ; 496
	i32 114, ; 497
	i32 46, ; 498
	i32 141, ; 499
	i32 116, ; 500
	i32 208, ; 501
	i32 33, ; 502
	i32 203, ; 503
	i32 175, ; 504
	i32 94, ; 505
	i32 52, ; 506
	i32 208, ; 507
	i32 261, ; 508
	i32 128, ; 509
	i32 152, ; 510
	i32 24, ; 511
	i32 160, ; 512
	i32 238, ; 513
	i32 147, ; 514
	i32 103, ; 515
	i32 88, ; 516
	i32 226, ; 517
	i32 59, ; 518
	i32 141, ; 519
	i32 99, ; 520
	i32 5, ; 521
	i32 13, ; 522
	i32 121, ; 523
	i32 134, ; 524
	i32 27, ; 525
	i32 302, ; 526
	i32 207, ; 527
	i32 192, ; 528
	i32 71, ; 529
	i32 236, ; 530
	i32 24, ; 531
	i32 224, ; 532
	i32 265, ; 533
	i32 262, ; 534
	i32 319, ; 535
	i32 136, ; 536
	i32 217, ; 537
	i32 233, ; 538
	i32 167, ; 539
	i32 266, ; 540
	i32 298, ; 541
	i32 206, ; 542
	i32 100, ; 543
	i32 122, ; 544
	i32 237, ; 545
	i32 179, ; 546
	i32 180, ; 547
	i32 162, ; 548
	i32 166, ; 549
	i32 240, ; 550
	i32 38, ; 551
	i32 195, ; 552
	i32 306, ; 553
	i32 17, ; 554
	i32 170, ; 555
	i32 319, ; 556
	i32 318, ; 557
	i32 136, ; 558
	i32 149, ; 559
	i32 229, ; 560
	i32 154, ; 561
	i32 129, ; 562
	i32 19, ; 563
	i32 64, ; 564
	i32 204, ; 565
	i32 146, ; 566
	i32 46, ; 567
	i32 326, ; 568
	i32 215, ; 569
	i32 78, ; 570
	i32 60, ; 571
	i32 105, ; 572
	i32 264, ; 573
	i32 219, ; 574
	i32 48, ; 575
	i32 250, ; 576
	i32 323, ; 577
	i32 261, ; 578
	i32 14, ; 579
	i32 178, ; 580
	i32 67, ; 581
	i32 170, ; 582
	i32 330, ; 583
	i32 225, ; 584
	i32 229, ; 585
	i32 328, ; 586
	i32 191, ; 587
	i32 77, ; 588
	i32 234, ; 589
	i32 107, ; 590
	i32 218, ; 591
	i32 260, ; 592
	i32 66, ; 593
	i32 62, ; 594
	i32 210, ; 595
	i32 159, ; 596
	i32 227, ; 597
	i32 10, ; 598
	i32 195, ; 599
	i32 11, ; 600
	i32 172, ; 601
	i32 77, ; 602
	i32 125, ; 603
	i32 82, ; 604
	i32 181, ; 605
	i32 65, ; 606
	i32 106, ; 607
	i32 64, ; 608
	i32 127, ; 609
	i32 121, ; 610
	i32 76, ; 611
	i32 275, ; 612
	i32 265, ; 613
	i32 327, ; 614
	i32 8, ; 615
	i32 233, ; 616
	i32 2, ; 617
	i32 43, ; 618
	i32 278, ; 619
	i32 155, ; 620
	i32 127, ; 621
	i32 263, ; 622
	i32 23, ; 623
	i32 132, ; 624
	i32 221, ; 625
	i32 252, ; 626
	i32 322, ; 627
	i32 329, ; 628
	i32 304, ; 629
	i32 28, ; 630
	i32 220, ; 631
	i32 0, ; 632
	i32 61, ; 633
	i32 197, ; 634
	i32 89, ; 635
	i32 86, ; 636
	i32 147, ; 637
	i32 199, ; 638
	i32 35, ; 639
	i32 85, ; 640
	i32 241, ; 641
	i32 317, ; 642
	i32 312, ; 643
	i32 180, ; 644
	i32 49, ; 645
	i32 6, ; 646
	i32 89, ; 647
	i32 324, ; 648
	i32 21, ; 649
	i32 161, ; 650
	i32 95, ; 651
	i32 49, ; 652
	i32 112, ; 653
	i32 257, ; 654
	i32 331, ; 655
	i32 129, ; 656
	i32 75, ; 657
	i32 210, ; 658
	i32 234, ; 659
	i32 256, ; 660
	i32 7, ; 661
	i32 211, ; 662
	i32 196, ; 663
	i32 174, ; 664
	i32 109, ; 665
	i32 257, ; 666
	i32 243 ; 667
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
