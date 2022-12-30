# Lucid Input
Simple input system for Unity

[![license](https://img.shields.io/badge/LICENSE-MIT-green.svg)](LICENSE)

<img src="https://github.com/AnnulusGames/LucidInput/blob/main/Assets/LucidInput/Documentation~/Header.png" width="800">

## 概要

Lucid InputはUnityのInputクラスを拡張し、同時押しやフリック、ダブルクリックなどの高度な入力を従来のInputクラスのようにコード一行で取得することを可能にします。

### 特徴
* シンプルで扱いやすいシステム
* 同時押しやフリックなどの高度な入力に対応
* Input Systemに対応、UnityEngine.Inputとの切り替えが可能

## セットアップ

### 要件
* Unity 2019.4 以上

### インストール
1. Window > Package ManagerからPackage Managerを開く
2. 「+」ボタン > Add package from git URL
3. 以下を入力する
   * https://github.com/AnnulusGames/LucidInput.git?path=/Assets/LucidInput


あるいはPackages/manifest.jsonを開き、dependenciesブロックに以下を追記

```json
{
    "dependencies": {
        "com.annulusgames.lucid-input": "https://github.com/AnnulusGames/LucidInput.git?path=/Assets/LucidInput"
    }
}
```

## Input Systemに対応
Input Systemを導入すると、Lucid Inputは自動的にInput SyetemのAPIを利用するように切り替わります。もしLucid Inputがどちらを利用するかを切り替えたい場合は、LucidInput.activeInputHandlingを変更してください。

```cs
//InputManager(UnityEngine.Input)を使用
LucidInput.activeInputHandling = InputHandlingMode.InputManager;

//InputSystemを使用
LucidInput.activeInputHandling = InputHandlingMode.InputSystem;
```

ゲームパッドでの入力を行いたい場合は、Input Systemの導入が必須になります。


## キー入力

### 基本的な入力

キーの入力を取得する場合、以下のように記述します。
```cs
if (LucidInput.GetKey(Key.Space))
{
    Debug.Log("space key is held down");
}
```

LucidInputでは、独自の列挙型「Key」を用いてキーを識別します。
Keyは、以下のようにKeyCodeやUnityEngine.InputSystem.Keyに変換可能です。

```cs
Key spaceKey = Key.Space;

//Key -> KeyCodeに変換
if (Input.GetKeyDown(spaceKey.ToKeyCode()))
{
    Debug.Log("space key was pressed");
}

//Key -> UnityEngine.InputSystem.Keyに変換
if (Keyboard.current[spaceKey.ToISKey()].wasPressedThisFrame)
{
    Debug.Log("space key was pressed");
}
```

押された瞬間や離された瞬間を取得したい場合は、従来のInputクラスと同様にGetKeyDown、GetKeyUpを利用します。

```cs
if (LucidInput.GetKeyDown(Key.A))
{
    Debug.Log("A key was pressed");
}
if (LucidInput.GetKeyUp(Key.B))
{
    Debug.Log("B key was released");
}
```

### 複数のキー

複数のキーを判別する場合はGetKeyAny、GetKeyAllを利用します。

```cs
//A、B、Cのいずれかのキーが押されている
if (LucidInput.GetKeyAny(Key.A, Key.B, Key.C))
{
    Debug.Log("A key or B key or C key is held down");
}

//AキーとBキーが同時に押された
if (LucidInput.GetKeyDownAll(Key.A, Key.B))
{
    Debug.Log("A key and B key was released");
}
```

同時押しを判定する時間を調整したい場合は、LucidInput.simultaneousPressIntervalの値を変更します。(初期値は0.1[s])
```cs
//同時押しを判定する時間を0.15sに変更
LucidInput.simultaneousPressInterval = 0.15f;
```

### タップ

キーが一定の時間押された後に離された(タップされた)瞬間を取得した場合は、GetKeyTapを利用します。
```cs
//Spaceキーが一定の時間押された後に離された
if (LucidInput.GetKeyTap(Key.Space))
{
    Debug.Log("tap");
}
```
ダブルタップを取得する場合はGetKeyDoubleTap、それ以上の回数のタップを取得する場合はGetKeyMultiTapを利用します。
```cs
//ダブルタップ
if (LucidInput.GetKeyDouleTap(Key.Space))
{
    Debug.Log("double tap");
}

//4回のタップ
if (LucidInput.GetKeyMultiTap(Key.Space, 4))
{
    Debug.Log("4 tap");
}
```

また、タップされた回数を取得したい場合はGetKeyTapCountを利用します。
```cs
int tapCount = LucidInput.GetKeyTapCount(Key.Space);
```

タップを判定する時間を調整したい場合はLucidInput.tapTime、複数回のタップの間隔を変更したい場合はLucidInput.tapIntervalの値を変更します。(初期値はどちらも0.2[s])

```cs
LucidInput.tapTime = 0.25f;
LucidInput.tapInterval = 0.15f;
```

### ホールド

キーの長押しを取得する場合は、GetKeyHold、GetKeyHoldEndedを利用します。

```cs
//スペースキーが1.0秒押された後、離されるまで毎フレームtrueを返す
if (LucidInput.GetKeyHold(Key.Space, 1.0f))
{
    Debug.Log("hold");
}

//スペースキーが1.0秒押された後、離された瞬間にtrueを返す
if (LucidInput.GetKeyHoldEnded(Key.Space, 1.0f))
{
    Debug.Log("hold ended");
}
```

また、キーが押されている時間を取得したい場合はGetKeyHoldTimeを利用します。

```cs
float time = LucidInput.GetKeyHoldTime(Key.Space);
```

## マウス入力

### ボタン

マウスのボタンの入力は以下のように行います。

```cs
if (LucidInput.GetMouseButton(0))
{
    Debug.Log("Pressed left click.");
}

if (LucidInput.GetMouseButton(1))
{
    Debug.Log("Pressed right click.");
}

if (LucidInput.GetMouseButton(2))
{
    Debug.Log("Pressed middle click.");
}
```

マウスのボタンもタップやホールドに対応しています。
```cs
if (LucidInput.GetMouseButtonDoubleTap(0))
{
    Debug.Log("double click.");
}

if (LucidInput.GetMouseButtonHoldEnded(1))
{
    Debug.Log("right button hold");
}
```

### フリック

マウスを利用したフリック操作を取得することも可能です。

```cs
//上下左右のいずれかの方向にフリック
if (LucidInput.GetMouseFlick(0))
{
    Debug.Log("flick");
}

//指定された方向にフリック
if (LucidInput.GetMouseFlick(0, Direction.Right))
{
    Debug.Log("right flick");
}
```

フリック操作を判定する距離や時間については、それぞれLucidInput.flickMinDistance、LucidInput.flickTimeを変更することで調整が可能です。

```cs
LucidInput.flickMinDistance = 80;
LucidInput.flickTime = 0.25f;
```

デフォルトではマウス操作をタッチでシミュレートするように設定されているため、GetMouse系のメソッドはタッチスクリーンでも動作します。無効にしたい場合は、LucidInput.simulateMouseWithTouchesをfalseに設定してください。

```cs
LucidInput.simulateMouseWithTouches = false;
```

### マウスポインター・ホイール

マウスポインターの位置や移動量を取得したい場合は、以下のように記述します。

```cs
//マウスポインターの位置をスクリーン座標で取得
Vector2 position = LucidInput.mousePosition;

//マウスポインターの移動量を取得
Vector2 delta = LucidInput.mousePositionDelta;
```

マウスホイールの変化量を取得する場合はmouseScrollDeltaを利用します。
```cs
//マウスホイールの変化量を取得
Vector2 mouseWheelDelta = LucidInput.mouseScrollDelta;
```

## タッチスクリーン

※Input Systemを使用する場合、タッチ情報の取得にはEnhancedTouchを利用するためEnhancedTouchSupportが有効化されている必要があります。(activeInputHandlingをInputSystemに設定すると自動で有効化されます)

画面のタッチの情報を取得したい場合は以下のように記述します。

```cs
//画面上のタッチの数だけ繰り返す
for (int i = 0; i < LucidInput.touchCount; i++)
{
    //タッチ情報を取得 (画面上にタッチが存在しない場合はnullを返す)
    Touch touch = LucidInput.GetTouch(i);
}
```

また、タッチもホールドやダブルタップなどに対応しています。それらを取得したい場合は、GetTouchButtonを利用します。

```cs
if (LucidInput.GetTouchButtonHold(0, 1.0f))
{
    Debug.Log("hold");
}

if (LucidInput.GetTouchButtonDoubleTap(0))
{
     Debug.Log("double tap");
}
```

フリック操作を取得したい場合はGetTouchFlickを利用します。

```cs
if (LucidInput.GetTouchFlick(0, Direction.Left))
{
    Debug.Log("left flick");
}
```

ピンチ操作(画面に2本の指をタッチして広げる・狭める動作)にも対応しています。ピンチ操作の変化量を取得したい場合は、pinchDeltaから取得できます。(画面に2本以上の指がない場合は0を返します)

```cs
float delta = LucidInput.pinchDelta;
```

## ゲームパッド
※ゲームパッドの入力にはInput Systemの導入が必須です。LucidInput.activeInputHandlingがInputSystemに設定されている場合にのみ動作します。

ゲームパッドのボタンの入力を取得する場合は、GetGamepadButtonを使用します。

```cs
if (LucidInput.GetGamepadButtonDown(GamepadButton.A))
{
    Debug.Log("A button was pressed");
}
```

ゲームパッドのボタンも他のボタン同様、高度な入力に対応しています。

```cs
if (LucidInput.GetGamepadButtonHoldEnded(GamepadButton.RightTrigger, 1f))
{
    Debug.Log("hold ended");
}

if (LucidInput.GetGamepadButtonDoubleTap(GamepadButton.DpadDown))
{
    Debug.Log("double tap");
}
```

また、GetKeyと同様にAny、Allも利用できます。

```cs
if (LucidInput.GetGamepadButtonDownAny(GamepadButton.A, GamepadButton.B))
{
    Debug.Log("A button or B button was pressed");
}

if (LucidInput.GetGamepadButtonDownAll(GamepadButton.RightTrigger, GamepadButton.LeftTrigger))
{
    Debug.Log("RT and LT was pressed");
}
```

スティックの値やトリガーの値を取得したい場合は、以下のように記述します。
```cs
//左スティックの値を取得
Vector2 stickValue = LucidInput.GetGamepadStick(LR.Left);

//右トリガーの値を取得
float triggerValue =  LucidInput.GetGamepadTrigger(LR.Right);
```

## Axis
Lucid Inputでは以下の5つのAxisが利用可能です。
* Horizontal
* Vertical
* MouseX
* MouseY
* MouseScrollWheel

```cs
//キー入力の場合、平滑化された値を返す
float value = LucidInput.GetAxis(Axis.Horizontal);

//-1, 0, 1のいずれかを返す
float valueRaw = LucidInput.GetAxisRaw(Axis.Vertical);
```

GetAxisによって取得できる値は独自の計算方式で算出されるため、従来のInput.GetAxisとは値が異なる場合があります。

## ライセンス

[Mit License](LICENSE)
