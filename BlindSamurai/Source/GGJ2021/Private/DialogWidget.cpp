// Fill out your copyright notice in the Description page of Project Settings.


#include "DialogWidget.h"


UDialogWidget::UDialogWidget(const FObjectInitializer& ObjectInitializer):Super(ObjectInitializer)
{
	Character_1Image=nullptr;
	Character_2Image=nullptr;
	DialogContentTextBlock=nullptr;

}

void UDialogWidget::NativeConstruct()
{
	Super::NativeConstruct();

	//根据组件ID查找Image组件
	if (UImage* img = Cast<UImage>(GetWidgetFromName(FName(TEXT("Image_Charactor_1")))))
	{
		Character_1Image = img;
	}
	if (UImage* img = Cast<UImage>(GetWidgetFromName(FName(TEXT("Image_Charactor_2")))))
	{	
		Character_2Image = img;
	}
	if (UTextBlock* tex = Cast<UTextBlock>(GetWidgetFromName(FName(TEXT("Dialog")))))
	{
		DialogContentTextBlock = tex;
	}
	if (UBorder* tex = Cast<UBorder>(GetWidgetFromName(FName(TEXT("DialogBorder")))))
	{
		DialogContentBorder = tex;
	}
}