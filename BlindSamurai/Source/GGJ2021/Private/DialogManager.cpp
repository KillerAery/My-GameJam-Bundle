// Fill out your copyright notice in the Description page of Project Settings.


#include "DialogManager.h"


#include "Components/Image.h"
#include "Components/TextBlock.h"
#include "GGJ2021/Public/DialogWidget.h"
class UDialogWidget;

UDialogManager::~UDialogManager()
{
	// 这块踩坑，手动清一下
	CurrentWidget = nullptr;
}

void UDialogManager::Initialize(FSubsystemCollectionBase& Collection)
{
	Super::Initialize(Collection);
	// 没法在构造函数里实例化别的蓝图类
	//static ConstructorHelpers::FClassFinder<UUserWidget> StartingWidgetClassFinder(TEXT("/Game/UMG/DialogTemplete"));
	//StartingWidgetClass = LoadClass<UUserWidget>(NULL, TEXT("WidgetBlueprint'/Game/UMG/DialogTemplete.DialogTemplete_C'"));
	//CurrentWidget = CreateWidget<UUserWidget>(GetWorld(), StartingWidgetClass);
	//CreateDialog(FirstTable);
	//InitPanel();
	CurrentWidget = nullptr;
	CurrentRow=0;
	MaxRow=0;
}

void UDialogManager::CreateDialog(FString TablePath)
{
	if (CurrentWidget != nullptr)
	{
		UDialogWidget* DialogPanel =  Cast<UDialogWidget>(CurrentWidget);
		FString TmpString(TEXT("什么也灭有"));
		LoadDialogContent(TmpString);
		if (DialogPanel != nullptr)
		{
			DialogPanel->AddToViewport();
		}
		ChangeState(EDialogState::BeforeDialogBegin);
		CurrentDataTable = LoadObject<UDataTable>(NULL, *(FString::Printf(TEXT("%s"), *TablePath)));//Table路径
		// 这是写死了，不应该，后续改掉
		//Character_1Tex = LoadObject<UTexture2D>(NULL, TEXT("Texture2D'/Game/UMG/Patorit_lailai.Patorit_lailai'"));
		//Character_2Tex = LoadObject<UTexture2D>(NULL, TEXT("Texture2D'/Game/UMG/Patorit_Enemy.Patorit_Enemy'"));
	}
	else
	{
		// 第一次使用创建Class
		DialogWidgetClass = LoadClass<UUserWidget>(NULL, TEXT("WidgetBlueprint'/Game/UMG/DialogTemplete.DialogTemplete_C'"));
		CurrentWidget = CreateWidget<UUserWidget>(GetWorld(), DialogWidgetClass);

		CreateDialog(TablePath);
		InitPanel();
	}
	
}


void UDialogManager::TickListenStateChange(float DeltaSeconds)
{
	FString ContextString;
	switch (PanelState)
	{
	case EDialogState::BeforeDialogBegin:
	{
		//Read from table	
		//Pause Game
		if (CurrentDataTable!=nullptr)
		{			
			RowNames = CurrentDataTable->GetRowNames();
			if (RowNames.Num() != 0)
			{
				NameData = RowNames.GetData();
				MaxRow = RowNames.Num();
				CurrentRow = 0;
			}
			else
			{
				ChangeState(EDialogState::Finish);
			}
		}	
		else {
			ChangeState(EDialogState::Finish);
		}
		InitPanel();
		ChangeState(EDialogState::DialogBegin);
		break;
	}
	case EDialogState::DialogBegin:
	{
		//load text 
		//load image
		FDialogDataRow* pRow = CurrentDataTable->FindRow<FDialogDataRow>(NameData[CurrentRow], ContextString);
		SetTextBlockVisibility(ESlateVisibility::Visible);
		LoadDialogContent(pRow->Content);
		if (pRow->Character_1Tex!=nullptr)
		{
			LoadCharacter_1Tex(pRow->Character_1Tex);
		}
		if (pRow->Character_2Tex!=nullptr)
		{
			LoadCharacter_2Tex(pRow->Character_2Tex);
		}	
			
		if (pRow->Character_1!=0) SetCharacter_1Visibility(ESlateVisibility::Visible);
		else  SetCharacter_1Visibility(ESlateVisibility::Hidden);
			
		if (pRow->Character_2 != 0) SetCharacter_2Visibility(ESlateVisibility::Visible);
		else  SetCharacter_2Visibility(ESlateVisibility::Hidden);
			
		if (pRow->Content.Len()!=0) SetBorderVisibility(ESlateVisibility::Visible);
		else  SetBorderVisibility(ESlateVisibility::Hidden);
			
		ChangeState(EDialogState::Typing);
		bIsClicked = false;
		break;
	}
	case EDialogState::Typing:
	{
		//Receive Click from blueprint
		if (bIsClicked)
		{
			ChangeState(EDialogState::Finish);
			bIsClicked = false;
			CurrentRow += 1;
		}
		break;
	}	
	case EDialogState::Finish:
	{
		InitPanel();
		if (CurrentRow < MaxRow)
		{
			ChangeState(EDialogState::DialogBegin);
		}
		else {
			//GEngine->AddOnScreenDebugMessage(-1, 5.0f, FColor::Blue, FString::Printf(TEXT("一段对话读完了小老弟")));
		}
		//unload resource ,actually we don't really care
		break;
	}		
	default:
		break;
	}

}

void UDialogManager::PrintTest()
{
	UE_LOG(LogTemp,Log,TEXT("%s"),*FString(__FUNCTION__))
}

void UDialogManager::ReceiveClickSignal(bool bIsClick)
{
	bIsClicked  = bIsClick;
}

void UDialogManager::InitPanel()
{
	SetCharacter_1Visibility(ESlateVisibility::Collapsed);
	SetCharacter_2Visibility(ESlateVisibility::Collapsed);
	SetTextBlockVisibility(ESlateVisibility::Collapsed);
	SetBorderVisibility(ESlateVisibility::Collapsed);
}
void UDialogManager::SetCharacter_1Visibility(ESlateVisibility Visibility)
{
	UDialogWidget* DialogPanel = Cast<UDialogWidget>(CurrentWidget);
	if (DialogPanel->Character_1Image)
	{
		DialogPanel->Character_1Image->SetVisibility(Visibility);
	}
	else {
		UE_LOG(LogTemp,Warning,TEXT("%s"),*FString("Charactor_1 Did Not Find"));
	}
}

void UDialogManager::SetCharacter_2Visibility(ESlateVisibility Visibility)
{
	UDialogWidget* DialogPanel = Cast<UDialogWidget>(CurrentWidget);
	if (DialogPanel->Character_2Image)
	{
		DialogPanel->Character_2Image->SetVisibility(Visibility);
	}
	else {
		UE_LOG(LogTemp,Warning,TEXT("%s"),*FString("Charactor_2 Did Not Find"));
	}
}
void UDialogManager::SetTextBlockVisibility(ESlateVisibility Visibility)
{
	UDialogWidget* DialogPanel = Cast<UDialogWidget>(CurrentWidget);
	if (DialogPanel->DialogContentTextBlock)
	{
		DialogPanel->DialogContentTextBlock->SetVisibility(Visibility);
	}
	else {
		UE_LOG(LogTemp,Warning,TEXT("%s"),*FString("TextBlock Did Not Find"));
	}
}

void UDialogManager::SetBorderVisibility(ESlateVisibility Visibility)
{
	UDialogWidget* DialogPanel = Cast<UDialogWidget>(CurrentWidget);
	if (DialogPanel->DialogContentBorder)
	{
		DialogPanel->DialogContentBorder->SetVisibility(Visibility);
	}
	else {
		UE_LOG(LogTemp,Warning,TEXT("%s"),*FString("DialogContentBorder Did Not Find"));
	}
}

void UDialogManager::LoadDialogContent(FString DialogContent) const
{
	UDialogWidget* DialogPanel =  Cast<UDialogWidget>(CurrentWidget);
	if (DialogPanel->DialogContentTextBlock)
		DialogPanel->DialogContentTextBlock->SetText(FText::FromString(DialogContent));
}

void UDialogManager::LoadCharacter_1Tex(UTexture2D* CharacterTex) const
{
	UDialogWidget* DialogPanel = Cast<UDialogWidget>(CurrentWidget);
	if (DialogPanel->Character_1Image)
		DialogPanel->Character_1Image->SetBrushFromTexture(CharacterTex);
}
void UDialogManager::LoadCharacter_2Tex(UTexture2D* CharacterTex) const
{
	UDialogWidget* DialogPanel = Cast<UDialogWidget>(CurrentWidget);
	if (DialogPanel->Character_2Image)
		DialogPanel->Character_2Image->SetBrushFromTexture(CharacterTex);
}
