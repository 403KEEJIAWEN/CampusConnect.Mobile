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

@assembly_image_cache = dso_local local_unnamed_addr global [314 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [622 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 68
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 67
	i32 15721112, ; 2: System.Runtime.Intrinsics.dll => 0xefe298 => 108
	i32 32687329, ; 3: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 230
	i32 34715100, ; 4: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 264
	i32 34839235, ; 5: System.IO.FileSystem.DriveInfo => 0x2139ac3 => 48
	i32 39485524, ; 6: System.Net.WebSockets.dll => 0x25a8054 => 80
	i32 42639949, ; 7: System.Threading.Thread => 0x28aa24d => 145
	i32 66541672, ; 8: System.Diagnostics.StackTrace => 0x3f75868 => 30
	i32 67008169, ; 9: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 305
	i32 68219467, ; 10: System.Security.Cryptography.Primitives => 0x410f24b => 124
	i32 72070932, ; 11: Microsoft.Maui.Graphics.dll => 0x44bb714 => 188
	i32 82292897, ; 12: System.Runtime.CompilerServices.VisualC.dll => 0x4e7b0a1 => 102
	i32 101534019, ; 13: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 248
	i32 117431740, ; 14: System.Runtime.InteropServices => 0x6ffddbc => 107
	i32 120558881, ; 15: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 248
	i32 122350210, ; 16: System.Threading.Channels.dll => 0x74aea82 => 139
	i32 134690465, ; 17: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 268
	i32 142721839, ; 18: System.Net.WebHeaderCollection => 0x881c32f => 77
	i32 149972175, ; 19: System.Security.Cryptography.Primitives.dll => 0x8f064cf => 124
	i32 159306688, ; 20: System.ComponentModel.Annotations => 0x97ed3c0 => 13
	i32 165246403, ; 21: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 204
	i32 176265551, ; 22: System.ServiceProcess => 0xa81994f => 132
	i32 182336117, ; 23: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 250
	i32 184328833, ; 24: System.ValueTuple.dll => 0xafca281 => 151
	i32 195452805, ; 25: vi/Microsoft.Maui.Controls.resources.dll => 0xba65f85 => 302
	i32 199333315, ; 26: zh-HK/Microsoft.Maui.Controls.resources.dll => 0xbe195c3 => 303
	i32 205061960, ; 27: System.ComponentModel => 0xc38ff48 => 18
	i32 209399409, ; 28: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 202
	i32 220171995, ; 29: System.Diagnostics.Debug => 0xd1f8edb => 26
	i32 230216969, ; 30: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 224
	i32 230752869, ; 31: Microsoft.CSharp.dll => 0xdc10265 => 1
	i32 231409092, ; 32: System.Linq.Parallel => 0xdcb05c4 => 59
	i32 231814094, ; 33: System.Globalization => 0xdd133ce => 42
	i32 246610117, ; 34: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 91
	i32 261689757, ; 35: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 207
	i32 276479776, ; 36: System.Threading.Timer.dll => 0x107abf20 => 147
	i32 278686392, ; 37: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 226
	i32 280482487, ; 38: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 223
	i32 280992041, ; 39: cs/Microsoft.Maui.Controls.resources.dll => 0x10bf9929 => 274
	i32 291076382, ; 40: System.IO.Pipes.AccessControl.dll => 0x1159791e => 54
	i32 298918909, ; 41: System.Net.Ping.dll => 0x11d123fd => 69
	i32 317674968, ; 42: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 302
	i32 318968648, ; 43: Xamarin.AndroidX.Activity.dll => 0x13031348 => 193
	i32 321597661, ; 44: System.Numerics => 0x132b30dd => 83
	i32 336156722, ; 45: ja/Microsoft.Maui.Controls.resources.dll => 0x14095832 => 287
	i32 342366114, ; 46: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 225
	i32 356389973, ; 47: it/Microsoft.Maui.Controls.resources.dll => 0x153e1455 => 286
	i32 360082299, ; 48: System.ServiceModel.Web => 0x15766b7b => 131
	i32 367780167, ; 49: System.IO.Pipes => 0x15ebe147 => 55
	i32 374914964, ; 50: System.Transactions.Local => 0x1658bf94 => 149
	i32 375677976, ; 51: System.Net.ServicePoint.dll => 0x16646418 => 74
	i32 379916513, ; 52: System.Threading.Thread.dll => 0x16a510e1 => 145
	i32 385762202, ; 53: System.Memory.dll => 0x16fe439a => 62
	i32 392610295, ; 54: System.Threading.ThreadPool.dll => 0x1766c1f7 => 146
	i32 395744057, ; 55: _Microsoft.Android.Resource.Designer => 0x17969339 => 310
	i32 403441872, ; 56: WindowsBase => 0x180c08d0 => 165
	i32 435591531, ; 57: sv/Microsoft.Maui.Controls.resources.dll => 0x19f6996b => 298
	i32 441335492, ; 58: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 208
	i32 442565967, ; 59: System.Collections => 0x1a61054f => 12
	i32 450948140, ; 60: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 221
	i32 451504562, ; 61: System.Security.Cryptography.X509Certificates => 0x1ae969b2 => 125
	i32 456227837, ; 62: System.Web.HttpUtility.dll => 0x1b317bfd => 152
	i32 459347974, ; 63: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 113
	i32 465846621, ; 64: mscorlib => 0x1bc4415d => 166
	i32 469710990, ; 65: System.dll => 0x1bff388e => 164
	i32 476646585, ; 66: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 223
	i32 486930444, ; 67: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 236
	i32 498788369, ; 68: System.ObjectModel => 0x1dbae811 => 84
	i32 500358224, ; 69: id/Microsoft.Maui.Controls.resources.dll => 0x1dd2dc50 => 285
	i32 503918385, ; 70: fi/Microsoft.Maui.Controls.resources.dll => 0x1e092f31 => 279
	i32 513247710, ; 71: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 182
	i32 526420162, ; 72: System.Transactions.dll => 0x1f6088c2 => 150
	i32 527452488, ; 73: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 268
	i32 530272170, ; 74: System.Linq.Queryable => 0x1f9b4faa => 60
	i32 539058512, ; 75: Microsoft.Extensions.Logging => 0x20216150 => 178
	i32 540030774, ; 76: System.IO.FileSystem.dll => 0x20303736 => 51
	i32 545304856, ; 77: System.Runtime.Extensions => 0x2080b118 => 103
	i32 546455878, ; 78: System.Runtime.Serialization.Xml => 0x20924146 => 114
	i32 549171840, ; 79: System.Globalization.Calendars => 0x20bbb280 => 40
	i32 557405415, ; 80: Jsr305Binding => 0x213954e7 => 261
	i32 569601784, ; 81: Xamarin.AndroidX.Window.Extensions.Core.Core => 0x21f36ef8 => 259
	i32 577335427, ; 82: System.Security.Cryptography.Cng => 0x22697083 => 120
	i32 592146354, ; 83: pt-BR/Microsoft.Maui.Controls.resources.dll => 0x234b6fb2 => 293
	i32 601371474, ; 84: System.IO.IsolatedStorage.dll => 0x23d83352 => 52
	i32 605376203, ; 85: System.IO.Compression.FileSystem => 0x24154ecb => 44
	i32 613668793, ; 86: System.Security.Cryptography.Algorithms => 0x2493d7b9 => 119
	i32 627609679, ; 87: Xamarin.AndroidX.CustomView => 0x2568904f => 213
	i32 627931235, ; 88: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 291
	i32 639843206, ; 89: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x26233b86 => 219
	i32 643868501, ; 90: System.Net => 0x2660a755 => 81
	i32 662205335, ; 91: System.Text.Encodings.Web.dll => 0x27787397 => 136
	i32 663517072, ; 92: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 255
	i32 666292255, ; 93: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 200
	i32 672442732, ; 94: System.Collections.Concurrent => 0x2814a96c => 8
	i32 683518922, ; 95: System.Net.Security => 0x28bdabca => 73
	i32 688181140, ; 96: ca/Microsoft.Maui.Controls.resources.dll => 0x2904cf94 => 273
	i32 690569205, ; 97: System.Xml.Linq.dll => 0x29293ff5 => 155
	i32 691348768, ; 98: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 270
	i32 693804605, ; 99: System.Windows => 0x295a9e3d => 154
	i32 699345723, ; 100: System.Reflection.Emit => 0x29af2b3b => 92
	i32 700284507, ; 101: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 265
	i32 700358131, ; 102: System.IO.Compression.ZipFile => 0x29be9df3 => 45
	i32 706645707, ; 103: ko/Microsoft.Maui.Controls.resources.dll => 0x2a1e8ecb => 288
	i32 709557578, ; 104: de/Microsoft.Maui.Controls.resources.dll => 0x2a4afd4a => 276
	i32 720511267, ; 105: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 269
	i32 722857257, ; 106: System.Runtime.Loader.dll => 0x2b15ed29 => 109
	i32 735137430, ; 107: System.Security.SecureString.dll => 0x2bd14e96 => 129
	i32 752232764, ; 108: System.Diagnostics.Contracts.dll => 0x2cd6293c => 25
	i32 755313932, ; 109: Xamarin.Android.Glide.Annotations.dll => 0x2d052d0c => 190
	i32 759454413, ; 110: System.Net.Requests => 0x2d445acd => 72
	i32 762598435, ; 111: System.IO.Pipes.dll => 0x2d745423 => 55
	i32 775507847, ; 112: System.IO.Compression => 0x2e394f87 => 46
	i32 777317022, ; 113: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 297
	i32 789151979, ; 114: Microsoft.Extensions.Options => 0x2f0980eb => 181
	i32 790371945, ; 115: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 0x2f1c1e69 => 214
	i32 804715423, ; 116: System.Data.Common => 0x2ff6fb9f => 22
	i32 807930345, ; 117: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 0x302809e9 => 228
	i32 823281589, ; 118: System.Private.Uri.dll => 0x311247b5 => 86
	i32 830298997, ; 119: System.IO.Compression.Brotli => 0x317d5b75 => 43
	i32 832635846, ; 120: System.Xml.XPath.dll => 0x31a103c6 => 160
	i32 834051424, ; 121: System.Net.Quic => 0x31b69d60 => 71
	i32 843511501, ; 122: Xamarin.AndroidX.Print => 0x3246f6cd => 241
	i32 873119928, ; 123: Microsoft.VisualBasic => 0x340ac0b8 => 3
	i32 877678880, ; 124: System.Globalization.dll => 0x34505120 => 42
	i32 878954865, ; 125: System.Net.Http.Json => 0x3463c971 => 63
	i32 904024072, ; 126: System.ComponentModel.Primitives.dll => 0x35e25008 => 16
	i32 911108515, ; 127: System.IO.MemoryMappedFiles.dll => 0x364e69a3 => 53
	i32 926902833, ; 128: tr/Microsoft.Maui.Controls.resources.dll => 0x373f6a31 => 300
	i32 928116545, ; 129: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 264
	i32 952186615, ; 130: System.Runtime.InteropServices.JavaScript.dll => 0x38c136f7 => 105
	i32 956575887, ; 131: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 269
	i32 966729478, ; 132: Xamarin.Google.Crypto.Tink.Android => 0x399f1f06 => 262
	i32 967690846, ; 133: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 225
	i32 975236339, ; 134: System.Diagnostics.Tracing => 0x3a20ecf3 => 34
	i32 975874589, ; 135: System.Xml.XDocument => 0x3a2aaa1d => 158
	i32 986514023, ; 136: System.Private.DataContractSerialization.dll => 0x3acd0267 => 85
	i32 987214855, ; 137: System.Diagnostics.Tools => 0x3ad7b407 => 32
	i32 992768348, ; 138: System.Collections.dll => 0x3b2c715c => 12
	i32 994442037, ; 139: System.IO.FileSystem => 0x3b45fb35 => 51
	i32 1001831731, ; 140: System.IO.UnmanagedMemoryStream.dll => 0x3bb6bd33 => 56
	i32 1012816738, ; 141: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 245
	i32 1019214401, ; 142: System.Drawing => 0x3cbffa41 => 36
	i32 1028951442, ; 143: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 177
	i32 1029334545, ; 144: da/Microsoft.Maui.Controls.resources.dll => 0x3d5a6611 => 275
	i32 1031528504, ; 145: Xamarin.Google.ErrorProne.Annotations.dll => 0x3d7be038 => 263
	i32 1035644815, ; 146: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 198
	i32 1036536393, ; 147: System.Drawing.Primitives.dll => 0x3dc84a49 => 35
	i32 1044663988, ; 148: System.Linq.Expressions.dll => 0x3e444eb4 => 58
	i32 1052210849, ; 149: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 232
	i32 1067306892, ; 150: GoogleGson => 0x3f9dcf8c => 173
	i32 1082857460, ; 151: System.ComponentModel.TypeConverter => 0x408b17f4 => 17
	i32 1084122840, ; 152: Xamarin.Kotlin.StdLib => 0x409e66d8 => 266
	i32 1098259244, ; 153: System => 0x41761b2c => 164
	i32 1118262833, ; 154: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 288
	i32 1121599056, ; 155: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 0x42da3e50 => 231
	i32 1127624469, ; 156: Microsoft.Extensions.Logging.Debug => 0x43362f15 => 180
	i32 1149092582, ; 157: Xamarin.AndroidX.Window => 0x447dc2e6 => 258
	i32 1168523401, ; 158: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 294
	i32 1170634674, ; 159: System.Web.dll => 0x45c677b2 => 153
	i32 1175144683, ; 160: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 254
	i32 1178241025, ; 161: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 239
	i32 1200138211, ; 162: CampusConnect.Mobile => 0x4788a7e3 => 0
	i32 1203215381, ; 163: pl/Microsoft.Maui.Controls.resources.dll => 0x47b79c15 => 292
	i32 1204270330, ; 164: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 200
	i32 1208641965, ; 165: System.Diagnostics.Process => 0x480a69ad => 29
	i32 1219128291, ; 166: System.IO.IsolatedStorage => 0x48aa6be3 => 52
	i32 1234928153, ; 167: nb/Microsoft.Maui.Controls.resources.dll => 0x499b8219 => 290
	i32 1243150071, ; 168: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 0x4a18f6f7 => 259
	i32 1253011324, ; 169: Microsoft.Win32.Registry => 0x4aaf6f7c => 5
	i32 1260983243, ; 170: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 274
	i32 1264511973, ; 171: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 249
	i32 1267360935, ; 172: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 253
	i32 1273260888, ; 173: Xamarin.AndroidX.Collection.Ktx => 0x4be46b58 => 205
	i32 1275534314, ; 174: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 270
	i32 1278448581, ; 175: Xamarin.AndroidX.Annotation.Jvm => 0x4c3393c5 => 197
	i32 1293217323, ; 176: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 216
	i32 1309188875, ; 177: System.Private.DataContractSerialization => 0x4e08a30b => 85
	i32 1322716291, ; 178: Xamarin.AndroidX.Window.dll => 0x4ed70c83 => 258
	i32 1324164729, ; 179: System.Linq => 0x4eed2679 => 61
	i32 1335329327, ; 180: System.Runtime.Serialization.Json.dll => 0x4f97822f => 112
	i32 1364015309, ; 181: System.IO => 0x514d38cd => 57
	i32 1373134921, ; 182: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 304
	i32 1376866003, ; 183: Xamarin.AndroidX.SavedState => 0x52114ed3 => 245
	i32 1379779777, ; 184: System.Resources.ResourceManager => 0x523dc4c1 => 99
	i32 1402170036, ; 185: System.Configuration.dll => 0x53936ab4 => 19
	i32 1406073936, ; 186: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 209
	i32 1408764838, ; 187: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 111
	i32 1411638395, ; 188: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 101
	i32 1422545099, ; 189: System.Runtime.CompilerServices.VisualC => 0x54ca50cb => 102
	i32 1430672901, ; 190: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 272
	i32 1434145427, ; 191: System.Runtime.Handles => 0x557b5293 => 104
	i32 1435222561, ; 192: Xamarin.Google.Crypto.Tink.Android.dll => 0x558bc221 => 262
	i32 1439761251, ; 193: System.Net.Quic.dll => 0x55d10363 => 71
	i32 1452070440, ; 194: System.Formats.Asn1.dll => 0x568cd628 => 38
	i32 1453312822, ; 195: System.Diagnostics.Tools.dll => 0x569fcb36 => 32
	i32 1457743152, ; 196: System.Runtime.Extensions.dll => 0x56e36530 => 103
	i32 1458022317, ; 197: System.Net.Security.dll => 0x56e7a7ad => 73
	i32 1461004990, ; 198: es\Microsoft.Maui.Controls.resources => 0x57152abe => 278
	i32 1461234159, ; 199: System.Collections.Immutable.dll => 0x5718a9ef => 9
	i32 1461719063, ; 200: System.Security.Cryptography.OpenSsl => 0x57201017 => 123
	i32 1462112819, ; 201: System.IO.Compression.dll => 0x57261233 => 46
	i32 1469204771, ; 202: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 199
	i32 1470490898, ; 203: Microsoft.Extensions.Primitives => 0x57a5e912 => 182
	i32 1479771757, ; 204: System.Collections.Immutable => 0x5833866d => 9
	i32 1480492111, ; 205: System.IO.Compression.Brotli.dll => 0x583e844f => 43
	i32 1487239319, ; 206: Microsoft.Win32.Primitives => 0x58a57897 => 4
	i32 1490025113, ; 207: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 0x58cffa99 => 246
	i32 1493001747, ; 208: hi/Microsoft.Maui.Controls.resources.dll => 0x58fd6613 => 282
	i32 1514721132, ; 209: el/Microsoft.Maui.Controls.resources.dll => 0x5a48cf6c => 277
	i32 1536373174, ; 210: System.Diagnostics.TextWriterTraceListener => 0x5b9331b6 => 31
	i32 1543031311, ; 211: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 138
	i32 1543355203, ; 212: System.Reflection.Emit.dll => 0x5bfdbb43 => 92
	i32 1550322496, ; 213: System.Reflection.Extensions.dll => 0x5c680b40 => 93
	i32 1551623176, ; 214: sk/Microsoft.Maui.Controls.resources.dll => 0x5c7be408 => 297
	i32 1565862583, ; 215: System.IO.FileSystem.Primitives => 0x5d552ab7 => 49
	i32 1566207040, ; 216: System.Threading.Tasks.Dataflow.dll => 0x5d5a6c40 => 141
	i32 1573704789, ; 217: System.Runtime.Serialization.Json => 0x5dccd455 => 112
	i32 1580037396, ; 218: System.Threading.Overlapped => 0x5e2d7514 => 140
	i32 1582372066, ; 219: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 215
	i32 1592978981, ; 220: System.Runtime.Serialization.dll => 0x5ef2ee25 => 115
	i32 1597949149, ; 221: Xamarin.Google.ErrorProne.Annotations => 0x5f3ec4dd => 263
	i32 1601112923, ; 222: System.Xml.Serialization => 0x5f6f0b5b => 157
	i32 1603525486, ; 223: Microsoft.Maui.Controls.HotReload.Forms.dll => 0x5f93db6e => 306
	i32 1604827217, ; 224: System.Net.WebClient => 0x5fa7b851 => 76
	i32 1618516317, ; 225: System.Net.WebSockets.Client.dll => 0x6078995d => 79
	i32 1622152042, ; 226: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 235
	i32 1622358360, ; 227: System.Dynamic.Runtime => 0x60b33958 => 37
	i32 1624863272, ; 228: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 257
	i32 1635184631, ; 229: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x6176eff7 => 219
	i32 1636350590, ; 230: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 212
	i32 1639515021, ; 231: System.Net.Http.dll => 0x61b9038d => 64
	i32 1639986890, ; 232: System.Text.RegularExpressions => 0x61c036ca => 138
	i32 1641389582, ; 233: System.ComponentModel.EventBasedAsync.dll => 0x61d59e0e => 15
	i32 1657153582, ; 234: System.Runtime => 0x62c6282e => 116
	i32 1658241508, ; 235: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 251
	i32 1658251792, ; 236: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 260
	i32 1670060433, ; 237: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 207
	i32 1675553242, ; 238: System.IO.FileSystem.DriveInfo.dll => 0x63dee9da => 48
	i32 1677501392, ; 239: System.Net.Primitives.dll => 0x63fca3d0 => 70
	i32 1678508291, ; 240: System.Net.WebSockets => 0x640c0103 => 80
	i32 1679769178, ; 241: System.Security.Cryptography => 0x641f3e5a => 126
	i32 1691477237, ; 242: System.Reflection.Metadata => 0x64d1e4f5 => 94
	i32 1696967625, ; 243: System.Security.Cryptography.Csp => 0x6525abc9 => 121
	i32 1698840827, ; 244: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 267
	i32 1701541528, ; 245: System.Diagnostics.Debug.dll => 0x656b7698 => 26
	i32 1720223769, ; 246: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 0x66888819 => 228
	i32 1726116996, ; 247: System.Reflection.dll => 0x66e27484 => 97
	i32 1728033016, ; 248: System.Diagnostics.FileVersionInfo.dll => 0x66ffb0f8 => 28
	i32 1729485958, ; 249: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 203
	i32 1736233607, ; 250: ro/Microsoft.Maui.Controls.resources.dll => 0x677cd287 => 295
	i32 1743415430, ; 251: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 273
	i32 1744735666, ; 252: System.Transactions.Local.dll => 0x67fe8db2 => 149
	i32 1746316138, ; 253: Mono.Android.Export => 0x6816ab6a => 169
	i32 1750313021, ; 254: Microsoft.Win32.Primitives.dll => 0x6853a83d => 4
	i32 1758240030, ; 255: System.Resources.Reader.dll => 0x68cc9d1e => 98
	i32 1763938596, ; 256: System.Diagnostics.TraceSource.dll => 0x69239124 => 33
	i32 1765942094, ; 257: System.Reflection.Extensions => 0x6942234e => 93
	i32 1766324549, ; 258: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 250
	i32 1770582343, ; 259: Microsoft.Extensions.Logging.dll => 0x6988f147 => 178
	i32 1776026572, ; 260: System.Core.dll => 0x69dc03cc => 21
	i32 1777075843, ; 261: System.Globalization.Extensions.dll => 0x69ec0683 => 41
	i32 1780572499, ; 262: Mono.Android.Runtime.dll => 0x6a216153 => 170
	i32 1782862114, ; 263: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 289
	i32 1788241197, ; 264: Xamarin.AndroidX.Fragment => 0x6a96652d => 221
	i32 1793755602, ; 265: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 281
	i32 1808609942, ; 266: Xamarin.AndroidX.Loader => 0x6bcd3296 => 235
	i32 1813058853, ; 267: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 266
	i32 1813201214, ; 268: Xamarin.Google.Android.Material => 0x6c13413e => 260
	i32 1818569960, ; 269: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 240
	i32 1818787751, ; 270: Microsoft.VisualBasic.Core => 0x6c687fa7 => 2
	i32 1824175904, ; 271: System.Text.Encoding.Extensions => 0x6cbab720 => 134
	i32 1824722060, ; 272: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 111
	i32 1827303595, ; 273: Microsoft.VisualStudio.DesignTools.TapContract => 0x6cea70ab => 308
	i32 1828688058, ; 274: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 179
	i32 1842015223, ; 275: uk/Microsoft.Maui.Controls.resources.dll => 0x6dcaebf7 => 301
	i32 1847515442, ; 276: Xamarin.Android.Glide.Annotations => 0x6e1ed932 => 190
	i32 1853025655, ; 277: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 298
	i32 1858542181, ; 278: System.Linq.Expressions => 0x6ec71a65 => 58
	i32 1870277092, ; 279: System.Reflection.Primitives => 0x6f7a29e4 => 95
	i32 1875935024, ; 280: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 280
	i32 1879696579, ; 281: System.Formats.Tar.dll => 0x7009e4c3 => 39
	i32 1885316902, ; 282: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 201
	i32 1885918049, ; 283: Microsoft.VisualStudio.DesignTools.TapContract.dll => 0x7068d361 => 308
	i32 1888955245, ; 284: System.Diagnostics.Contracts => 0x70972b6d => 25
	i32 1889954781, ; 285: System.Reflection.Metadata.dll => 0x70a66bdd => 94
	i32 1898237753, ; 286: System.Reflection.DispatchProxy => 0x7124cf39 => 89
	i32 1900610850, ; 287: System.Resources.ResourceManager.dll => 0x71490522 => 99
	i32 1910275211, ; 288: System.Collections.NonGeneric.dll => 0x71dc7c8b => 10
	i32 1939592360, ; 289: System.Private.Xml.Linq => 0x739bd4a8 => 87
	i32 1956758971, ; 290: System.Resources.Writer => 0x74a1c5bb => 100
	i32 1961813231, ; 291: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x74eee4ef => 247
	i32 1968388702, ; 292: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 174
	i32 1983156543, ; 293: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 267
	i32 1985761444, ; 294: Xamarin.Android.Glide.GifDecoder => 0x765c50a4 => 192
	i32 2003115576, ; 295: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 277
	i32 2011961780, ; 296: System.Buffers.dll => 0x77ec19b4 => 7
	i32 2019465201, ; 297: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 232
	i32 2025202353, ; 298: ar/Microsoft.Maui.Controls.resources.dll => 0x78b622b1 => 272
	i32 2031763787, ; 299: Xamarin.Android.Glide => 0x791a414b => 189
	i32 2045470958, ; 300: System.Private.Xml => 0x79eb68ee => 88
	i32 2055257422, ; 301: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 227
	i32 2060060697, ; 302: System.Windows.dll => 0x7aca0819 => 154
	i32 2066184531, ; 303: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 276
	i32 2070888862, ; 304: System.Diagnostics.TraceSource => 0x7b6f419e => 33
	i32 2079903147, ; 305: System.Runtime.dll => 0x7bf8cdab => 116
	i32 2090596640, ; 306: System.Numerics.Vectors => 0x7c9bf920 => 82
	i32 2117912485, ; 307: Microsoft.VisualStudio.DesignTools.XamlTapContract.dll => 0x7e3cc7a5 => 309
	i32 2127167465, ; 308: System.Console => 0x7ec9ffe9 => 20
	i32 2142473426, ; 309: System.Collections.Specialized => 0x7fb38cd2 => 11
	i32 2143790110, ; 310: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 162
	i32 2146852085, ; 311: Microsoft.VisualBasic.dll => 0x7ff65cf5 => 3
	i32 2159891885, ; 312: Microsoft.Maui => 0x80bd55ad => 186
	i32 2169148018, ; 313: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 284
	i32 2181898931, ; 314: Microsoft.Extensions.Options.dll => 0x820d22b3 => 181
	i32 2192057212, ; 315: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 179
	i32 2193016926, ; 316: System.ObjectModel.dll => 0x82b6c85e => 84
	i32 2201107256, ; 317: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 271
	i32 2201231467, ; 318: System.Net.Http => 0x8334206b => 64
	i32 2207618523, ; 319: it\Microsoft.Maui.Controls.resources => 0x839595db => 286
	i32 2217644978, ; 320: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 254
	i32 2222056684, ; 321: System.Threading.Tasks.Parallel => 0x8471e4ec => 143
	i32 2244775296, ; 322: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 236
	i32 2252106437, ; 323: System.Xml.Serialization.dll => 0x863c6ac5 => 157
	i32 2256313426, ; 324: System.Globalization.Extensions => 0x867c9c52 => 41
	i32 2265110946, ; 325: System.Security.AccessControl.dll => 0x8702d9a2 => 117
	i32 2266799131, ; 326: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 175
	i32 2267999099, ; 327: Xamarin.Android.Glide.DiskLruCache.dll => 0x872eeb7b => 191
	i32 2270573516, ; 328: fr/Microsoft.Maui.Controls.resources.dll => 0x875633cc => 280
	i32 2279755925, ; 329: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 243
	i32 2293034957, ; 330: System.ServiceModel.Web.dll => 0x88acefcd => 131
	i32 2295906218, ; 331: System.Net.Sockets => 0x88d8bfaa => 75
	i32 2298471582, ; 332: System.Net.Mail => 0x88ffe49e => 66
	i32 2303942373, ; 333: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 290
	i32 2305521784, ; 334: System.Private.CoreLib.dll => 0x896b7878 => 172
	i32 2315684594, ; 335: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 195
	i32 2320631194, ; 336: System.Threading.Tasks.Parallel.dll => 0x8a52059a => 143
	i32 2337881282, ; 337: CampusConnect.Mobile.dll => 0x8b593cc2 => 0
	i32 2340441535, ; 338: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 106
	i32 2344264397, ; 339: System.ValueTuple => 0x8bbaa2cd => 151
	i32 2353062107, ; 340: System.Net.Primitives => 0x8c40e0db => 70
	i32 2368005991, ; 341: System.Xml.ReaderWriter.dll => 0x8d24e767 => 156
	i32 2371007202, ; 342: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 174
	i32 2378619854, ; 343: System.Security.Cryptography.Csp.dll => 0x8dc6dbce => 121
	i32 2383496789, ; 344: System.Security.Principal.Windows.dll => 0x8e114655 => 127
	i32 2395872292, ; 345: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 285
	i32 2401565422, ; 346: System.Web.HttpUtility => 0x8f24faee => 152
	i32 2403452196, ; 347: Xamarin.AndroidX.Emoji2.dll => 0x8f41c524 => 218
	i32 2409983638, ; 348: Microsoft.VisualStudio.DesignTools.MobileTapContracts.dll => 0x8fa56e96 => 307
	i32 2421380589, ; 349: System.Threading.Tasks.Dataflow => 0x905355ed => 141
	i32 2423080555, ; 350: Xamarin.AndroidX.Collection.Ktx.dll => 0x906d466b => 205
	i32 2427813419, ; 351: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 282
	i32 2435356389, ; 352: System.Console.dll => 0x912896e5 => 20
	i32 2435904999, ; 353: System.ComponentModel.DataAnnotations.dll => 0x9130f5e7 => 14
	i32 2454642406, ; 354: System.Text.Encoding.dll => 0x924edee6 => 135
	i32 2458678730, ; 355: System.Net.Sockets.dll => 0x928c75ca => 75
	i32 2459001652, ; 356: System.Linq.Parallel.dll => 0x92916334 => 59
	i32 2465532216, ; 357: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 208
	i32 2471841756, ; 358: netstandard.dll => 0x93554fdc => 167
	i32 2475788418, ; 359: Java.Interop.dll => 0x93918882 => 168
	i32 2480646305, ; 360: Microsoft.Maui.Controls => 0x93dba8a1 => 184
	i32 2483903535, ; 361: System.ComponentModel.EventBasedAsync => 0x940d5c2f => 15
	i32 2484371297, ; 362: System.Net.ServicePoint => 0x94147f61 => 74
	i32 2490993605, ; 363: System.AppContext.dll => 0x94798bc5 => 6
	i32 2501346920, ; 364: System.Data.DataSetExtensions => 0x95178668 => 23
	i32 2505896520, ; 365: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 230
	i32 2522472828, ; 366: Xamarin.Android.Glide.dll => 0x9659e17c => 189
	i32 2538310050, ; 367: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 91
	i32 2550873716, ; 368: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 283
	i32 2562349572, ; 369: Microsoft.CSharp => 0x98ba5a04 => 1
	i32 2570120770, ; 370: System.Text.Encodings.Web => 0x9930ee42 => 136
	i32 2581783588, ; 371: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 0x99e2e424 => 231
	i32 2581819634, ; 372: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 253
	i32 2585220780, ; 373: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 134
	i32 2585805581, ; 374: System.Net.Ping => 0x9a20430d => 69
	i32 2589602615, ; 375: System.Threading.ThreadPool => 0x9a5a3337 => 146
	i32 2593496499, ; 376: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 292
	i32 2605712449, ; 377: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 271
	i32 2615233544, ; 378: Xamarin.AndroidX.Fragment.Ktx => 0x9be14c08 => 222
	i32 2616218305, ; 379: Microsoft.Extensions.Logging.Debug.dll => 0x9bf052c1 => 180
	i32 2617129537, ; 380: System.Private.Xml.dll => 0x9bfe3a41 => 88
	i32 2618712057, ; 381: System.Reflection.TypeExtensions.dll => 0x9c165ff9 => 96
	i32 2620871830, ; 382: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 212
	i32 2624644809, ; 383: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 217
	i32 2626831493, ; 384: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 287
	i32 2627185994, ; 385: System.Diagnostics.TextWriterTraceListener.dll => 0x9c97ad4a => 31
	i32 2629843544, ; 386: System.IO.Compression.ZipFile.dll => 0x9cc03a58 => 45
	i32 2633051222, ; 387: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 226
	i32 2663391936, ; 388: Xamarin.Android.Glide.DiskLruCache => 0x9ec022c0 => 191
	i32 2663698177, ; 389: System.Runtime.Loader => 0x9ec4cf01 => 109
	i32 2664396074, ; 390: System.Xml.XDocument.dll => 0x9ecf752a => 158
	i32 2665622720, ; 391: System.Drawing.Primitives => 0x9ee22cc0 => 35
	i32 2676780864, ; 392: System.Data.Common.dll => 0x9f8c6f40 => 22
	i32 2686887180, ; 393: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 114
	i32 2693849962, ; 394: System.IO.dll => 0xa090e36a => 57
	i32 2701096212, ; 395: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 251
	i32 2715334215, ; 396: System.Threading.Tasks.dll => 0xa1d8b647 => 144
	i32 2717744543, ; 397: System.Security.Claims => 0xa1fd7d9f => 118
	i32 2719963679, ; 398: System.Security.Cryptography.Cng.dll => 0xa21f5a1f => 120
	i32 2724373263, ; 399: System.Runtime.Numerics.dll => 0xa262a30f => 110
	i32 2732626843, ; 400: Xamarin.AndroidX.Activity => 0xa2e0939b => 193
	i32 2735172069, ; 401: System.Threading.Channels => 0xa30769e5 => 139
	i32 2737747696, ; 402: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 199
	i32 2740948882, ; 403: System.IO.Pipes.AccessControl => 0xa35f8f92 => 54
	i32 2748088231, ; 404: System.Runtime.InteropServices.JavaScript => 0xa3cc7fa7 => 105
	i32 2752995522, ; 405: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 293
	i32 2758225723, ; 406: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 185
	i32 2764765095, ; 407: Microsoft.Maui.dll => 0xa4caf7a7 => 186
	i32 2765824710, ; 408: System.Text.Encoding.CodePages.dll => 0xa4db22c6 => 133
	i32 2770495804, ; 409: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 265
	i32 2778768386, ; 410: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 256
	i32 2779977773, ; 411: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0xa5b3182d => 244
	i32 2785988530, ; 412: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 299
	i32 2788224221, ; 413: Xamarin.AndroidX.Fragment.Ktx.dll => 0xa630ecdd => 222
	i32 2801831435, ; 414: Microsoft.Maui.Graphics => 0xa7008e0b => 188
	i32 2803228030, ; 415: System.Xml.XPath.XDocument.dll => 0xa715dd7e => 159
	i32 2806116107, ; 416: es/Microsoft.Maui.Controls.resources.dll => 0xa741ef0b => 278
	i32 2810250172, ; 417: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 209
	i32 2819470561, ; 418: System.Xml.dll => 0xa80db4e1 => 163
	i32 2821205001, ; 419: System.ServiceProcess.dll => 0xa8282c09 => 132
	i32 2821294376, ; 420: Xamarin.AndroidX.ResourceInspection.Annotation => 0xa8298928 => 244
	i32 2824502124, ; 421: System.Xml.XmlDocument => 0xa85a7b6c => 161
	i32 2831556043, ; 422: nl/Microsoft.Maui.Controls.resources.dll => 0xa8c61dcb => 291
	i32 2838993487, ; 423: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 0xa9379a4f => 233
	i32 2849599387, ; 424: System.Threading.Overlapped.dll => 0xa9d96f9b => 140
	i32 2853208004, ; 425: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 256
	i32 2855708567, ; 426: Xamarin.AndroidX.Transition => 0xaa36a797 => 252
	i32 2861098320, ; 427: Mono.Android.Export.dll => 0xaa88e550 => 169
	i32 2861189240, ; 428: Microsoft.Maui.Essentials => 0xaa8a4878 => 187
	i32 2870099610, ; 429: Xamarin.AndroidX.Activity.Ktx.dll => 0xab123e9a => 194
	i32 2875164099, ; 430: Jsr305Binding.dll => 0xab5f85c3 => 261
	i32 2875220617, ; 431: System.Globalization.Calendars.dll => 0xab606289 => 40
	i32 2884993177, ; 432: Xamarin.AndroidX.ExifInterface => 0xabf58099 => 220
	i32 2887636118, ; 433: System.Net.dll => 0xac1dd496 => 81
	i32 2899753641, ; 434: System.IO.UnmanagedMemoryStream => 0xacd6baa9 => 56
	i32 2900621748, ; 435: System.Dynamic.Runtime.dll => 0xace3f9b4 => 37
	i32 2901442782, ; 436: System.Reflection => 0xacf080de => 97
	i32 2905242038, ; 437: mscorlib.dll => 0xad2a79b6 => 166
	i32 2909740682, ; 438: System.Private.CoreLib => 0xad6f1e8a => 172
	i32 2916838712, ; 439: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 257
	i32 2919462931, ; 440: System.Numerics.Vectors.dll => 0xae037813 => 82
	i32 2921128767, ; 441: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 196
	i32 2936416060, ; 442: System.Resources.Reader => 0xaf06273c => 98
	i32 2940926066, ; 443: System.Diagnostics.StackTrace.dll => 0xaf4af872 => 30
	i32 2942453041, ; 444: System.Xml.XPath.XDocument => 0xaf624531 => 159
	i32 2959614098, ; 445: System.ComponentModel.dll => 0xb0682092 => 18
	i32 2968338931, ; 446: System.Security.Principal.Windows => 0xb0ed41f3 => 127
	i32 2972252294, ; 447: System.Security.Cryptography.Algorithms.dll => 0xb128f886 => 119
	i32 2978675010, ; 448: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 216
	i32 2987532451, ; 449: Xamarin.AndroidX.Security.SecurityCrypto => 0xb21220a3 => 247
	i32 2996846495, ; 450: Xamarin.AndroidX.Lifecycle.Process.dll => 0xb2a03f9f => 229
	i32 3016983068, ; 451: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 249
	i32 3023353419, ; 452: WindowsBase.dll => 0xb434b64b => 165
	i32 3024354802, ; 453: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 224
	i32 3038032645, ; 454: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 310
	i32 3056245963, ; 455: Xamarin.AndroidX.SavedState.SavedState.Ktx => 0xb62a9ccb => 246
	i32 3057625584, ; 456: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 237
	i32 3059408633, ; 457: Mono.Android.Runtime => 0xb65adef9 => 170
	i32 3059793426, ; 458: System.ComponentModel.Primitives => 0xb660be12 => 16
	i32 3075834255, ; 459: System.Threading.Tasks => 0xb755818f => 144
	i32 3077302341, ; 460: hu/Microsoft.Maui.Controls.resources.dll => 0xb76be845 => 284
	i32 3090735792, ; 461: System.Security.Cryptography.X509Certificates.dll => 0xb838e2b0 => 125
	i32 3099732863, ; 462: System.Security.Claims.dll => 0xb8c22b7f => 118
	i32 3103600923, ; 463: System.Formats.Asn1 => 0xb8fd311b => 38
	i32 3111772706, ; 464: System.Runtime.Serialization => 0xb979e222 => 115
	i32 3121463068, ; 465: System.IO.FileSystem.AccessControl.dll => 0xba0dbf1c => 47
	i32 3124832203, ; 466: System.Threading.Tasks.Extensions => 0xba4127cb => 142
	i32 3132293585, ; 467: System.Security.AccessControl => 0xbab301d1 => 117
	i32 3147165239, ; 468: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 34
	i32 3148237826, ; 469: GoogleGson.dll => 0xbba64c02 => 173
	i32 3159123045, ; 470: System.Reflection.Primitives.dll => 0xbc4c6465 => 95
	i32 3160747431, ; 471: System.IO.MemoryMappedFiles => 0xbc652da7 => 53
	i32 3178803400, ; 472: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 238
	i32 3192346100, ; 473: System.Security.SecureString => 0xbe4755f4 => 129
	i32 3193515020, ; 474: System.Web => 0xbe592c0c => 153
	i32 3204380047, ; 475: System.Data.dll => 0xbefef58f => 24
	i32 3209718065, ; 476: System.Xml.XmlDocument.dll => 0xbf506931 => 161
	i32 3211777861, ; 477: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 215
	i32 3217618498, ; 478: Microsoft.VisualStudio.DesignTools.XamlTapContract => 0xbfc8f642 => 309
	i32 3220365878, ; 479: System.Threading => 0xbff2e236 => 148
	i32 3226221578, ; 480: System.Runtime.Handles.dll => 0xc04c3c0a => 104
	i32 3251039220, ; 481: System.Reflection.DispatchProxy.dll => 0xc1c6ebf4 => 89
	i32 3258312781, ; 482: Xamarin.AndroidX.CardView => 0xc235e84d => 203
	i32 3265493905, ; 483: System.Linq.Queryable.dll => 0xc2a37b91 => 60
	i32 3265893370, ; 484: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 142
	i32 3277815716, ; 485: System.Resources.Writer.dll => 0xc35f7fa4 => 100
	i32 3279906254, ; 486: Microsoft.Win32.Registry.dll => 0xc37f65ce => 5
	i32 3280506390, ; 487: System.ComponentModel.Annotations.dll => 0xc3888e16 => 13
	i32 3290767353, ; 488: System.Security.Cryptography.Encoding => 0xc4251ff9 => 122
	i32 3299363146, ; 489: System.Text.Encoding => 0xc4a8494a => 135
	i32 3303498502, ; 490: System.Diagnostics.FileVersionInfo => 0xc4e76306 => 28
	i32 3305363605, ; 491: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 279
	i32 3316684772, ; 492: System.Net.Requests.dll => 0xc5b097e4 => 72
	i32 3317135071, ; 493: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 213
	i32 3317144872, ; 494: System.Data => 0xc5b79d28 => 24
	i32 3340431453, ; 495: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 201
	i32 3345895724, ; 496: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 242
	i32 3346324047, ; 497: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 239
	i32 3357674450, ; 498: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 296
	i32 3358260929, ; 499: System.Text.Json => 0xc82afec1 => 137
	i32 3362336904, ; 500: Xamarin.AndroidX.Activity.Ktx => 0xc8693088 => 194
	i32 3362522851, ; 501: Xamarin.AndroidX.Core => 0xc86c06e3 => 210
	i32 3366347497, ; 502: Java.Interop => 0xc8a662e9 => 168
	i32 3374999561, ; 503: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 243
	i32 3381016424, ; 504: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 275
	i32 3395150330, ; 505: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 101
	i32 3403906625, ; 506: System.Security.Cryptography.OpenSsl.dll => 0xcae37e41 => 123
	i32 3405233483, ; 507: Xamarin.AndroidX.CustomView.PoolingContainer => 0xcaf7bd4b => 214
	i32 3428513518, ; 508: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 176
	i32 3429136800, ; 509: System.Xml => 0xcc6479a0 => 163
	i32 3430777524, ; 510: netstandard => 0xcc7d82b4 => 167
	i32 3441283291, ; 511: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 217
	i32 3445260447, ; 512: System.Formats.Tar => 0xcd5a809f => 39
	i32 3452344032, ; 513: Microsoft.Maui.Controls.Compatibility.dll => 0xcdc696e0 => 183
	i32 3463511458, ; 514: hr/Microsoft.Maui.Controls.resources.dll => 0xce70fda2 => 283
	i32 3471940407, ; 515: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 17
	i32 3476120550, ; 516: Mono.Android => 0xcf3163e6 => 171
	i32 3479583265, ; 517: ru/Microsoft.Maui.Controls.resources.dll => 0xcf663a21 => 296
	i32 3484440000, ; 518: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 295
	i32 3485117614, ; 519: System.Text.Json.dll => 0xcfbaacae => 137
	i32 3486566296, ; 520: System.Transactions => 0xcfd0c798 => 150
	i32 3493954962, ; 521: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 206
	i32 3509114376, ; 522: System.Xml.Linq => 0xd128d608 => 155
	i32 3515174580, ; 523: System.Security.dll => 0xd1854eb4 => 130
	i32 3530912306, ; 524: System.Configuration => 0xd2757232 => 19
	i32 3539954161, ; 525: System.Net.HttpListener => 0xd2ff69f1 => 65
	i32 3560100363, ; 526: System.Threading.Timer => 0xd432d20b => 147
	i32 3570554715, ; 527: System.IO.FileSystem.AccessControl => 0xd4d2575b => 47
	i32 3580758918, ; 528: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 303
	i32 3597029428, ; 529: Xamarin.Android.Glide.GifDecoder.dll => 0xd6665034 => 192
	i32 3598340787, ; 530: System.Net.WebSockets.Client => 0xd67a52b3 => 79
	i32 3608519521, ; 531: System.Linq.dll => 0xd715a361 => 61
	i32 3624195450, ; 532: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 106
	i32 3627220390, ; 533: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 241
	i32 3633644679, ; 534: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 196
	i32 3638274909, ; 535: System.IO.FileSystem.Primitives.dll => 0xd8dbab5d => 49
	i32 3641597786, ; 536: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 227
	i32 3643446276, ; 537: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 300
	i32 3643854240, ; 538: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 238
	i32 3645089577, ; 539: System.ComponentModel.DataAnnotations => 0xd943a729 => 14
	i32 3657292374, ; 540: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 175
	i32 3660523487, ; 541: System.Net.NetworkInformation => 0xda2f27df => 68
	i32 3672681054, ; 542: Mono.Android.dll => 0xdae8aa5e => 171
	i32 3676670898, ; 543: Microsoft.Maui.Controls.HotReload.Forms => 0xdb258bb2 => 306
	i32 3682565725, ; 544: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 202
	i32 3684561358, ; 545: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 206
	i32 3697841164, ; 546: zh-Hant/Microsoft.Maui.Controls.resources.dll => 0xdc68940c => 305
	i32 3700866549, ; 547: System.Net.WebProxy.dll => 0xdc96bdf5 => 78
	i32 3706696989, ; 548: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 211
	i32 3716563718, ; 549: System.Runtime.Intrinsics => 0xdd864306 => 108
	i32 3718780102, ; 550: Xamarin.AndroidX.Annotation => 0xdda814c6 => 195
	i32 3724971120, ; 551: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 237
	i32 3732100267, ; 552: System.Net.NameResolution => 0xde7354ab => 67
	i32 3737834244, ; 553: System.Net.Http.Json.dll => 0xdecad304 => 63
	i32 3748608112, ; 554: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 27
	i32 3751444290, ; 555: System.Xml.XPath => 0xdf9a7f42 => 160
	i32 3786282454, ; 556: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 204
	i32 3792276235, ; 557: System.Collections.NonGeneric => 0xe2098b0b => 10
	i32 3800979733, ; 558: Microsoft.Maui.Controls.Compatibility => 0xe28e5915 => 183
	i32 3802395368, ; 559: System.Collections.Specialized.dll => 0xe2a3f2e8 => 11
	i32 3819260425, ; 560: System.Net.WebProxy => 0xe3a54a09 => 78
	i32 3823082795, ; 561: System.Security.Cryptography.dll => 0xe3df9d2b => 126
	i32 3829621856, ; 562: System.Numerics.dll => 0xe4436460 => 83
	i32 3841636137, ; 563: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 177
	i32 3844307129, ; 564: System.Net.Mail.dll => 0xe52378b9 => 66
	i32 3849253459, ; 565: System.Runtime.InteropServices.dll => 0xe56ef253 => 107
	i32 3870376305, ; 566: System.Net.HttpListener.dll => 0xe6b14171 => 65
	i32 3873536506, ; 567: System.Security.Principal => 0xe6e179fa => 128
	i32 3875112723, ; 568: System.Security.Cryptography.Encoding.dll => 0xe6f98713 => 122
	i32 3885497537, ; 569: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 77
	i32 3885922214, ; 570: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 252
	i32 3888767677, ; 571: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 242
	i32 3889960447, ; 572: zh-Hans/Microsoft.Maui.Controls.resources.dll => 0xe7dc15ff => 304
	i32 3896106733, ; 573: System.Collections.Concurrent.dll => 0xe839deed => 8
	i32 3896760992, ; 574: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 210
	i32 3901907137, ; 575: Microsoft.VisualBasic.Core.dll => 0xe89260c1 => 2
	i32 3920810846, ; 576: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 44
	i32 3921031405, ; 577: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 255
	i32 3928044579, ; 578: System.Xml.ReaderWriter => 0xea213423 => 156
	i32 3930554604, ; 579: System.Security.Principal.dll => 0xea4780ec => 128
	i32 3931092270, ; 580: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 240
	i32 3945713374, ; 581: System.Data.DataSetExtensions.dll => 0xeb2ecede => 23
	i32 3953953790, ; 582: System.Text.Encoding.CodePages => 0xebac8bfe => 133
	i32 3955647286, ; 583: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 198
	i32 3959773229, ; 584: Xamarin.AndroidX.Lifecycle.Process => 0xec05582d => 229
	i32 3980434154, ; 585: th/Microsoft.Maui.Controls.resources.dll => 0xed409aea => 299
	i32 3987592930, ; 586: he/Microsoft.Maui.Controls.resources.dll => 0xedadd6e2 => 281
	i32 4003436829, ; 587: System.Diagnostics.Process.dll => 0xee9f991d => 29
	i32 4015948917, ; 588: Xamarin.AndroidX.Annotation.Jvm.dll => 0xef5e8475 => 197
	i32 4025784931, ; 589: System.Memory => 0xeff49a63 => 62
	i32 4046471985, ; 590: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 185
	i32 4054681211, ; 591: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 90
	i32 4068434129, ; 592: System.Private.Xml.Linq.dll => 0xf27f60d1 => 87
	i32 4073602200, ; 593: System.Threading.dll => 0xf2ce3c98 => 148
	i32 4094352644, ; 594: Microsoft.Maui.Essentials.dll => 0xf40add04 => 187
	i32 4099507663, ; 595: System.Drawing.dll => 0xf45985cf => 36
	i32 4100113165, ; 596: System.Private.Uri => 0xf462c30d => 86
	i32 4101593132, ; 597: Xamarin.AndroidX.Emoji2 => 0xf479582c => 218
	i32 4102112229, ; 598: pt/Microsoft.Maui.Controls.resources.dll => 0xf48143e5 => 294
	i32 4125707920, ; 599: ms/Microsoft.Maui.Controls.resources.dll => 0xf5e94e90 => 289
	i32 4126470640, ; 600: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 176
	i32 4127667938, ; 601: System.IO.FileSystem.Watcher => 0xf60736e2 => 50
	i32 4130442656, ; 602: System.AppContext => 0xf6318da0 => 6
	i32 4147896353, ; 603: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 90
	i32 4150914736, ; 604: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 301
	i32 4151237749, ; 605: System.Core => 0xf76edc75 => 21
	i32 4159265925, ; 606: System.Xml.XmlSerializer => 0xf7e95c85 => 162
	i32 4161255271, ; 607: System.Reflection.TypeExtensions => 0xf807b767 => 96
	i32 4164802419, ; 608: System.IO.FileSystem.Watcher.dll => 0xf83dd773 => 50
	i32 4181436372, ; 609: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 113
	i32 4182413190, ; 610: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 234
	i32 4182880526, ; 611: Microsoft.VisualStudio.DesignTools.MobileTapContracts => 0xf951b10e => 307
	i32 4185676441, ; 612: System.Security => 0xf97c5a99 => 130
	i32 4196529839, ; 613: System.Net.WebClient.dll => 0xfa21f6af => 76
	i32 4213026141, ; 614: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 27
	i32 4256097574, ; 615: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 211
	i32 4258378803, ; 616: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 0xfdd1b433 => 233
	i32 4260525087, ; 617: System.Buffers => 0xfdf2741f => 7
	i32 4271975918, ; 618: Microsoft.Maui.Controls.dll => 0xfea12dee => 184
	i32 4274976490, ; 619: System.Runtime.Numerics => 0xfecef6ea => 110
	i32 4292120959, ; 620: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 234
	i32 4294763496 ; 621: Xamarin.AndroidX.ExifInterface.dll => 0xfffce3e8 => 220
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [622 x i32] [
	i32 68, ; 0
	i32 67, ; 1
	i32 108, ; 2
	i32 230, ; 3
	i32 264, ; 4
	i32 48, ; 5
	i32 80, ; 6
	i32 145, ; 7
	i32 30, ; 8
	i32 305, ; 9
	i32 124, ; 10
	i32 188, ; 11
	i32 102, ; 12
	i32 248, ; 13
	i32 107, ; 14
	i32 248, ; 15
	i32 139, ; 16
	i32 268, ; 17
	i32 77, ; 18
	i32 124, ; 19
	i32 13, ; 20
	i32 204, ; 21
	i32 132, ; 22
	i32 250, ; 23
	i32 151, ; 24
	i32 302, ; 25
	i32 303, ; 26
	i32 18, ; 27
	i32 202, ; 28
	i32 26, ; 29
	i32 224, ; 30
	i32 1, ; 31
	i32 59, ; 32
	i32 42, ; 33
	i32 91, ; 34
	i32 207, ; 35
	i32 147, ; 36
	i32 226, ; 37
	i32 223, ; 38
	i32 274, ; 39
	i32 54, ; 40
	i32 69, ; 41
	i32 302, ; 42
	i32 193, ; 43
	i32 83, ; 44
	i32 287, ; 45
	i32 225, ; 46
	i32 286, ; 47
	i32 131, ; 48
	i32 55, ; 49
	i32 149, ; 50
	i32 74, ; 51
	i32 145, ; 52
	i32 62, ; 53
	i32 146, ; 54
	i32 310, ; 55
	i32 165, ; 56
	i32 298, ; 57
	i32 208, ; 58
	i32 12, ; 59
	i32 221, ; 60
	i32 125, ; 61
	i32 152, ; 62
	i32 113, ; 63
	i32 166, ; 64
	i32 164, ; 65
	i32 223, ; 66
	i32 236, ; 67
	i32 84, ; 68
	i32 285, ; 69
	i32 279, ; 70
	i32 182, ; 71
	i32 150, ; 72
	i32 268, ; 73
	i32 60, ; 74
	i32 178, ; 75
	i32 51, ; 76
	i32 103, ; 77
	i32 114, ; 78
	i32 40, ; 79
	i32 261, ; 80
	i32 259, ; 81
	i32 120, ; 82
	i32 293, ; 83
	i32 52, ; 84
	i32 44, ; 85
	i32 119, ; 86
	i32 213, ; 87
	i32 291, ; 88
	i32 219, ; 89
	i32 81, ; 90
	i32 136, ; 91
	i32 255, ; 92
	i32 200, ; 93
	i32 8, ; 94
	i32 73, ; 95
	i32 273, ; 96
	i32 155, ; 97
	i32 270, ; 98
	i32 154, ; 99
	i32 92, ; 100
	i32 265, ; 101
	i32 45, ; 102
	i32 288, ; 103
	i32 276, ; 104
	i32 269, ; 105
	i32 109, ; 106
	i32 129, ; 107
	i32 25, ; 108
	i32 190, ; 109
	i32 72, ; 110
	i32 55, ; 111
	i32 46, ; 112
	i32 297, ; 113
	i32 181, ; 114
	i32 214, ; 115
	i32 22, ; 116
	i32 228, ; 117
	i32 86, ; 118
	i32 43, ; 119
	i32 160, ; 120
	i32 71, ; 121
	i32 241, ; 122
	i32 3, ; 123
	i32 42, ; 124
	i32 63, ; 125
	i32 16, ; 126
	i32 53, ; 127
	i32 300, ; 128
	i32 264, ; 129
	i32 105, ; 130
	i32 269, ; 131
	i32 262, ; 132
	i32 225, ; 133
	i32 34, ; 134
	i32 158, ; 135
	i32 85, ; 136
	i32 32, ; 137
	i32 12, ; 138
	i32 51, ; 139
	i32 56, ; 140
	i32 245, ; 141
	i32 36, ; 142
	i32 177, ; 143
	i32 275, ; 144
	i32 263, ; 145
	i32 198, ; 146
	i32 35, ; 147
	i32 58, ; 148
	i32 232, ; 149
	i32 173, ; 150
	i32 17, ; 151
	i32 266, ; 152
	i32 164, ; 153
	i32 288, ; 154
	i32 231, ; 155
	i32 180, ; 156
	i32 258, ; 157
	i32 294, ; 158
	i32 153, ; 159
	i32 254, ; 160
	i32 239, ; 161
	i32 0, ; 162
	i32 292, ; 163
	i32 200, ; 164
	i32 29, ; 165
	i32 52, ; 166
	i32 290, ; 167
	i32 259, ; 168
	i32 5, ; 169
	i32 274, ; 170
	i32 249, ; 171
	i32 253, ; 172
	i32 205, ; 173
	i32 270, ; 174
	i32 197, ; 175
	i32 216, ; 176
	i32 85, ; 177
	i32 258, ; 178
	i32 61, ; 179
	i32 112, ; 180
	i32 57, ; 181
	i32 304, ; 182
	i32 245, ; 183
	i32 99, ; 184
	i32 19, ; 185
	i32 209, ; 186
	i32 111, ; 187
	i32 101, ; 188
	i32 102, ; 189
	i32 272, ; 190
	i32 104, ; 191
	i32 262, ; 192
	i32 71, ; 193
	i32 38, ; 194
	i32 32, ; 195
	i32 103, ; 196
	i32 73, ; 197
	i32 278, ; 198
	i32 9, ; 199
	i32 123, ; 200
	i32 46, ; 201
	i32 199, ; 202
	i32 182, ; 203
	i32 9, ; 204
	i32 43, ; 205
	i32 4, ; 206
	i32 246, ; 207
	i32 282, ; 208
	i32 277, ; 209
	i32 31, ; 210
	i32 138, ; 211
	i32 92, ; 212
	i32 93, ; 213
	i32 297, ; 214
	i32 49, ; 215
	i32 141, ; 216
	i32 112, ; 217
	i32 140, ; 218
	i32 215, ; 219
	i32 115, ; 220
	i32 263, ; 221
	i32 157, ; 222
	i32 306, ; 223
	i32 76, ; 224
	i32 79, ; 225
	i32 235, ; 226
	i32 37, ; 227
	i32 257, ; 228
	i32 219, ; 229
	i32 212, ; 230
	i32 64, ; 231
	i32 138, ; 232
	i32 15, ; 233
	i32 116, ; 234
	i32 251, ; 235
	i32 260, ; 236
	i32 207, ; 237
	i32 48, ; 238
	i32 70, ; 239
	i32 80, ; 240
	i32 126, ; 241
	i32 94, ; 242
	i32 121, ; 243
	i32 267, ; 244
	i32 26, ; 245
	i32 228, ; 246
	i32 97, ; 247
	i32 28, ; 248
	i32 203, ; 249
	i32 295, ; 250
	i32 273, ; 251
	i32 149, ; 252
	i32 169, ; 253
	i32 4, ; 254
	i32 98, ; 255
	i32 33, ; 256
	i32 93, ; 257
	i32 250, ; 258
	i32 178, ; 259
	i32 21, ; 260
	i32 41, ; 261
	i32 170, ; 262
	i32 289, ; 263
	i32 221, ; 264
	i32 281, ; 265
	i32 235, ; 266
	i32 266, ; 267
	i32 260, ; 268
	i32 240, ; 269
	i32 2, ; 270
	i32 134, ; 271
	i32 111, ; 272
	i32 308, ; 273
	i32 179, ; 274
	i32 301, ; 275
	i32 190, ; 276
	i32 298, ; 277
	i32 58, ; 278
	i32 95, ; 279
	i32 280, ; 280
	i32 39, ; 281
	i32 201, ; 282
	i32 308, ; 283
	i32 25, ; 284
	i32 94, ; 285
	i32 89, ; 286
	i32 99, ; 287
	i32 10, ; 288
	i32 87, ; 289
	i32 100, ; 290
	i32 247, ; 291
	i32 174, ; 292
	i32 267, ; 293
	i32 192, ; 294
	i32 277, ; 295
	i32 7, ; 296
	i32 232, ; 297
	i32 272, ; 298
	i32 189, ; 299
	i32 88, ; 300
	i32 227, ; 301
	i32 154, ; 302
	i32 276, ; 303
	i32 33, ; 304
	i32 116, ; 305
	i32 82, ; 306
	i32 309, ; 307
	i32 20, ; 308
	i32 11, ; 309
	i32 162, ; 310
	i32 3, ; 311
	i32 186, ; 312
	i32 284, ; 313
	i32 181, ; 314
	i32 179, ; 315
	i32 84, ; 316
	i32 271, ; 317
	i32 64, ; 318
	i32 286, ; 319
	i32 254, ; 320
	i32 143, ; 321
	i32 236, ; 322
	i32 157, ; 323
	i32 41, ; 324
	i32 117, ; 325
	i32 175, ; 326
	i32 191, ; 327
	i32 280, ; 328
	i32 243, ; 329
	i32 131, ; 330
	i32 75, ; 331
	i32 66, ; 332
	i32 290, ; 333
	i32 172, ; 334
	i32 195, ; 335
	i32 143, ; 336
	i32 0, ; 337
	i32 106, ; 338
	i32 151, ; 339
	i32 70, ; 340
	i32 156, ; 341
	i32 174, ; 342
	i32 121, ; 343
	i32 127, ; 344
	i32 285, ; 345
	i32 152, ; 346
	i32 218, ; 347
	i32 307, ; 348
	i32 141, ; 349
	i32 205, ; 350
	i32 282, ; 351
	i32 20, ; 352
	i32 14, ; 353
	i32 135, ; 354
	i32 75, ; 355
	i32 59, ; 356
	i32 208, ; 357
	i32 167, ; 358
	i32 168, ; 359
	i32 184, ; 360
	i32 15, ; 361
	i32 74, ; 362
	i32 6, ; 363
	i32 23, ; 364
	i32 230, ; 365
	i32 189, ; 366
	i32 91, ; 367
	i32 283, ; 368
	i32 1, ; 369
	i32 136, ; 370
	i32 231, ; 371
	i32 253, ; 372
	i32 134, ; 373
	i32 69, ; 374
	i32 146, ; 375
	i32 292, ; 376
	i32 271, ; 377
	i32 222, ; 378
	i32 180, ; 379
	i32 88, ; 380
	i32 96, ; 381
	i32 212, ; 382
	i32 217, ; 383
	i32 287, ; 384
	i32 31, ; 385
	i32 45, ; 386
	i32 226, ; 387
	i32 191, ; 388
	i32 109, ; 389
	i32 158, ; 390
	i32 35, ; 391
	i32 22, ; 392
	i32 114, ; 393
	i32 57, ; 394
	i32 251, ; 395
	i32 144, ; 396
	i32 118, ; 397
	i32 120, ; 398
	i32 110, ; 399
	i32 193, ; 400
	i32 139, ; 401
	i32 199, ; 402
	i32 54, ; 403
	i32 105, ; 404
	i32 293, ; 405
	i32 185, ; 406
	i32 186, ; 407
	i32 133, ; 408
	i32 265, ; 409
	i32 256, ; 410
	i32 244, ; 411
	i32 299, ; 412
	i32 222, ; 413
	i32 188, ; 414
	i32 159, ; 415
	i32 278, ; 416
	i32 209, ; 417
	i32 163, ; 418
	i32 132, ; 419
	i32 244, ; 420
	i32 161, ; 421
	i32 291, ; 422
	i32 233, ; 423
	i32 140, ; 424
	i32 256, ; 425
	i32 252, ; 426
	i32 169, ; 427
	i32 187, ; 428
	i32 194, ; 429
	i32 261, ; 430
	i32 40, ; 431
	i32 220, ; 432
	i32 81, ; 433
	i32 56, ; 434
	i32 37, ; 435
	i32 97, ; 436
	i32 166, ; 437
	i32 172, ; 438
	i32 257, ; 439
	i32 82, ; 440
	i32 196, ; 441
	i32 98, ; 442
	i32 30, ; 443
	i32 159, ; 444
	i32 18, ; 445
	i32 127, ; 446
	i32 119, ; 447
	i32 216, ; 448
	i32 247, ; 449
	i32 229, ; 450
	i32 249, ; 451
	i32 165, ; 452
	i32 224, ; 453
	i32 310, ; 454
	i32 246, ; 455
	i32 237, ; 456
	i32 170, ; 457
	i32 16, ; 458
	i32 144, ; 459
	i32 284, ; 460
	i32 125, ; 461
	i32 118, ; 462
	i32 38, ; 463
	i32 115, ; 464
	i32 47, ; 465
	i32 142, ; 466
	i32 117, ; 467
	i32 34, ; 468
	i32 173, ; 469
	i32 95, ; 470
	i32 53, ; 471
	i32 238, ; 472
	i32 129, ; 473
	i32 153, ; 474
	i32 24, ; 475
	i32 161, ; 476
	i32 215, ; 477
	i32 309, ; 478
	i32 148, ; 479
	i32 104, ; 480
	i32 89, ; 481
	i32 203, ; 482
	i32 60, ; 483
	i32 142, ; 484
	i32 100, ; 485
	i32 5, ; 486
	i32 13, ; 487
	i32 122, ; 488
	i32 135, ; 489
	i32 28, ; 490
	i32 279, ; 491
	i32 72, ; 492
	i32 213, ; 493
	i32 24, ; 494
	i32 201, ; 495
	i32 242, ; 496
	i32 239, ; 497
	i32 296, ; 498
	i32 137, ; 499
	i32 194, ; 500
	i32 210, ; 501
	i32 168, ; 502
	i32 243, ; 503
	i32 275, ; 504
	i32 101, ; 505
	i32 123, ; 506
	i32 214, ; 507
	i32 176, ; 508
	i32 163, ; 509
	i32 167, ; 510
	i32 217, ; 511
	i32 39, ; 512
	i32 183, ; 513
	i32 283, ; 514
	i32 17, ; 515
	i32 171, ; 516
	i32 296, ; 517
	i32 295, ; 518
	i32 137, ; 519
	i32 150, ; 520
	i32 206, ; 521
	i32 155, ; 522
	i32 130, ; 523
	i32 19, ; 524
	i32 65, ; 525
	i32 147, ; 526
	i32 47, ; 527
	i32 303, ; 528
	i32 192, ; 529
	i32 79, ; 530
	i32 61, ; 531
	i32 106, ; 532
	i32 241, ; 533
	i32 196, ; 534
	i32 49, ; 535
	i32 227, ; 536
	i32 300, ; 537
	i32 238, ; 538
	i32 14, ; 539
	i32 175, ; 540
	i32 68, ; 541
	i32 171, ; 542
	i32 306, ; 543
	i32 202, ; 544
	i32 206, ; 545
	i32 305, ; 546
	i32 78, ; 547
	i32 211, ; 548
	i32 108, ; 549
	i32 195, ; 550
	i32 237, ; 551
	i32 67, ; 552
	i32 63, ; 553
	i32 27, ; 554
	i32 160, ; 555
	i32 204, ; 556
	i32 10, ; 557
	i32 183, ; 558
	i32 11, ; 559
	i32 78, ; 560
	i32 126, ; 561
	i32 83, ; 562
	i32 177, ; 563
	i32 66, ; 564
	i32 107, ; 565
	i32 65, ; 566
	i32 128, ; 567
	i32 122, ; 568
	i32 77, ; 569
	i32 252, ; 570
	i32 242, ; 571
	i32 304, ; 572
	i32 8, ; 573
	i32 210, ; 574
	i32 2, ; 575
	i32 44, ; 576
	i32 255, ; 577
	i32 156, ; 578
	i32 128, ; 579
	i32 240, ; 580
	i32 23, ; 581
	i32 133, ; 582
	i32 198, ; 583
	i32 229, ; 584
	i32 299, ; 585
	i32 281, ; 586
	i32 29, ; 587
	i32 197, ; 588
	i32 62, ; 589
	i32 185, ; 590
	i32 90, ; 591
	i32 87, ; 592
	i32 148, ; 593
	i32 187, ; 594
	i32 36, ; 595
	i32 86, ; 596
	i32 218, ; 597
	i32 294, ; 598
	i32 289, ; 599
	i32 176, ; 600
	i32 50, ; 601
	i32 6, ; 602
	i32 90, ; 603
	i32 301, ; 604
	i32 21, ; 605
	i32 162, ; 606
	i32 96, ; 607
	i32 50, ; 608
	i32 113, ; 609
	i32 234, ; 610
	i32 307, ; 611
	i32 130, ; 612
	i32 76, ; 613
	i32 27, ; 614
	i32 211, ; 615
	i32 233, ; 616
	i32 7, ; 617
	i32 184, ; 618
	i32 110, ; 619
	i32 234, ; 620
	i32 220 ; 621
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
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.4xx @ 82d8938cf80f6d5fa6c28529ddfbdb753d805ab4"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"NumRegisterParameters", i32 0}
